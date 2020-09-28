using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModernHttpClient;
using Newtonsoft.Json.Linq;
using Xamarin.Auth;
using System.Linq;

namespace Gitter.Android
{
    internal class GitterAuth : WebRedirectAuthenticator
    {
        private readonly Uri accessTokenUrl;
        private readonly Uri authorizeUrl;
        private readonly string clientId;
        private readonly string clientSecret;
        private readonly GetUsernameAsyncFunc getUsernameAsync;
        private readonly string requestState;
        private readonly string scope;
        private bool reportedForgery;

        public GitterAuth(GetUsernameAsyncFunc getUsernameAsync = null)
            : this("a0fc459712567a41ccd5fb8bbfbf35ce0ea6cb56",
            "fbf16dedfed97d1d059604cedaf8bfc2d69d3cbe",
            String.Empty,
            new Uri("https://gitter.im/login/oauth/authorize"),
            new Uri("http://oauth.gitter.flagbug.com"),
            new Uri("https://gitter.im/login/oauth/token"),
            getUsernameAsync)
        { }

        public GitterAuth(string clientId, string clientSecret, string scope, Uri authorizeUrl, Uri redirectUrl, Uri accessTokenUrl, GetUsernameAsyncFunc getUsernameAsync = null)
            : this(redirectUrl, clientSecret, accessTokenUrl)
        {
            if (string.IsNullOrEmpty(clientId))
            {
                throw new ArgumentException("clientId must be provided", "clientId");
            }
            this.clientId = clientId;

            if (string.IsNullOrEmpty(clientSecret))
            {
                throw new ArgumentException("clientSecret must be provided", "clientSecret");
            }
            this.clientSecret = clientSecret;

            this.scope = scope ?? "";

            if (authorizeUrl == null)
            {
                throw new ArgumentNullException("authorizeUrl");
            }
            this.authorizeUrl = authorizeUrl;

            if (accessTokenUrl == null)
            {
                throw new ArgumentNullException("accessTokenUrl");
            }
            this.accessTokenUrl = accessTokenUrl;

            this.getUsernameAsync = getUsernameAsync;
        }

        private GitterAuth(Uri redirectUrl, string clientSecret = null, Uri accessTokenUrl = null)
            : base(redirectUrl, redirectUrl)
        {
            this.clientSecret = clientSecret;

            this.accessTokenUrl = accessTokenUrl;

            //
            // Generate a unique state string to check for forgeries
            //
            var chars = new char[16];
            var rand = new Random();
            for (var i = 0; i < chars.Length; i++)
            {
                chars[i] = (char)rand.Next((int)'a', (int)'z' + 1);
            }
            this.requestState = new string(chars);
        }

        /// <summary>
        /// Gets the access token URL.
        /// </summary>
        /// <value>The URL used to request access tokens after an authorization code was received.</value>
        public Uri AccessTokenUrl
        {
            get { return this.accessTokenUrl; }
        }

        /// <summary>
        /// Gets the authorize URL.
        /// </summary>
        /// <value>The authorize URL.</value>
        public Uri AuthorizeUrl
        {
            get { return this.authorizeUrl; }
        }

        /// <summary>
        /// Gets the client identifier.
        /// </summary>
        /// <value>The client identifier.</value>
        public string ClientId
        {
            get { return this.clientId; }
        }

        /// <summary>
        /// Gets the client secret.
        /// </summary>
        /// <value>The client secret.</value>
        public string ClientSecret
        {
            get { return this.clientSecret; }
        }

        /// <summary>
        /// Gets the authorization scope.
        /// </summary>
        /// <value>The authorization scope.</value>
        public string Scope
        {
            get { return this.scope; }
        }

        private bool IsImplicit { get { return accessTokenUrl == null; } }

        /// <summary>
        /// Method that returns the initial URL to be displayed in the web browser.
        /// </summary>
        /// <returns>A task that will return the initial URL.</returns>
        public override Task<Uri> GetInitialUrlAsync()
        {
            var url = new Uri(string.Format(
                "{0}?client_id={1}&redirect_uri={2}&response_type={3}&scope={4}&state={5}",
                authorizeUrl,
                clientId,
                RedirectUrl.OriginalString,
                IsImplicit ? "token" : "code",
                scope,
                requestState));

            return Task.FromResult(url);
        }

        /// <summary>
        /// Raised when a new page has been loaded.
        /// </summary>
        /// <param name="url">URL of the page.</param>
        /// <param name="query">The parsed query of the URL.</param>
        /// <param name="fragment">The parsed fragment of the URL.</param>
        protected override void OnPageEncountered(Uri url, IDictionary<string, string> query, IDictionary<string, string> fragment)
        {
            var all = new Dictionary<string, string>(query);
            foreach (var kv in fragment)
                all[kv.Key] = kv.Value;

            //
            // Check for forgeries
            //
            if (all.ContainsKey("state"))
            {
                if (all["state"] != requestState && !reportedForgery)
                {
                    reportedForgery = true;
                    OnError("Invalid state from server. Possible forgery!");
                    return;
                }
            }

            //
            // Continue processing
            //
            base.OnPageEncountered(url, query, fragment);
        }

        /// <summary>
        /// Raised when a new page has been loaded.
        /// </summary>
        /// <param name="url">URL of the page.</param>
        /// <param name="query">The parsed query string of the URL.</param>
        /// <param name="fragment">The parsed fragment of the URL.</param>
        protected override void OnRedirectPageLoaded(Uri url, IDictionary<string, string> query, IDictionary<string, string> fragment)
        {
            //
            // Look for the access_token
            //
            if (fragment.ContainsKey("access_token"))
            {
                //
                // We found an access_token
                //
                OnRetrievedAccountProperties(fragment);
            }
            else if (!IsImplicit)
            {
                //
                // Look for the code
                //
                if (query.ContainsKey("code"))
                {
                    var code = query["code"];
                    RequestAccessTokenAsync(code).ContinueWith(task =>
                    {
                        if (task.IsFaulted)
                        {
                            OnError(task.Exception);
                        }
                        else
                        {
                            OnRetrievedAccountProperties(task.Result);
                        }
                    }, TaskScheduler.FromCurrentSynchronizationContext());
                }
                else
                {
                    OnError("Expected code in response, but did not receive one.");
                    return;
                }
            }
            else
            {
                OnError("Expected access_token in response, but did not receive one.");
                return;
            }
        }

        /// <summary>
        /// Event handler that is fired when an access token has been retreived.
        /// </summary>
        /// <param name="accountProperties">The retrieved account properties</param>
        protected virtual void OnRetrievedAccountProperties(IDictionary<string, string> accountProperties)
        {
            //
            // Now we just need a username for the account
            //
            if (getUsernameAsync != null)
            {
                getUsernameAsync(accountProperties).ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        OnError(task.Exception);
                    }
                    else
                    {
                        OnSucceeded(task.Result, accountProperties);
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            else
            {
                OnSucceeded("", accountProperties);
            }
        }

        /// <summary>
        /// Asynchronously makes a request to the access token URL with the given parameters.
        /// </summary>
        /// <param name="queryValues">The parameters to make the request with.</param>
        /// <returns>The data provided in the response to the access token request.</returns>
        protected async Task<IDictionary<string, string>> RequestAccessTokenAsync(IDictionary<string, string> queryValues)
        {
            var query = queryValues.FormEncode();

            using (var client = new HttpClient(new NativeMessageHandler()))
            {
                var content = new StringContent(query, Encoding.UTF8, "application/x-www-form-urlencoded");

                HttpResponseMessage response = await client.PostAsync(this.accessTokenUrl, content);

                string responseContent = await response.Content.ReadAsStringAsync();

                IDictionary<string, JToken> data = JObject.Parse(responseContent);

                if (data["error"] != null)
                {
                    throw new AuthException("Error authenticating: " + data["error"]);
                }

                if (data["access_token"] != null)
                {
                    return data.ToDictionary(pair => pair.Key, pair => pair.Value.ToString());
                }

                throw new AuthException("Expected access_token in access token response, but did not receive one.");
            }
        }

        /// <summary>
        /// Asynchronously requests an access token with an authorization <paramref name="code" /> .
        /// </summary>
        /// <returns>A dictionary of data returned from the authorization request.</returns>
        /// <param name="code">The authorization code.</param>
        /// <remarks>Implements: http://tools.ietf.org/html/rfc6749#section-4.1</remarks>
        private Task<IDictionary<string, string>> RequestAccessTokenAsync(string code)
        {
            var queryValues = new Dictionary<string, string> {
				{ "grant_type", "authorization_code" },
				{ "code", code },
				{ "redirect_uri", RedirectUrl.OriginalString },
				{ "client_id", clientId },
			};
            if (!string.IsNullOrEmpty(clientSecret))
            {
                queryValues["client_secret"] = clientSecret;
            }

            return RequestAccessTokenAsync(queryValues);
        }
    }
}