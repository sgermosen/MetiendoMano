using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;
[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]
namespace PsTwitterClient.iOS
{
    public class LoginPageRenderer : PageRenderer
    {
        bool showLogin = true;
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (showLogin && App.User == null)
            {
                //Twitter with OAuth1
                var auth = new OAuth1Authenticator(
                    consumerKey: "Add your consumer key from Twitter",
                    consumerSecret: "Add your consumer secrete from Twitter",
                    requestTokenUrl: new Uri("https://api.twitter.com/oauth/request_token"),
                    authorizeUrl: new Uri("https://api.twitter.com/oauth/authorize"),
                    accessTokenUrl: new Uri("https://api.twitter.com/oauth/access_token"),
                    callbackUrl: new Uri("http://twitter.com")
                );

                auth.Completed += (sender, eventArgs) =>
                {
                    DismissViewController(true, null);

                    if (eventArgs.IsAuthenticated)
                    {
                        App.User = new Entities.UserDetails();
                        App.User.Token = eventArgs.Account.Properties["oauth_token"];
                        App.User.TokenSecret = eventArgs.Account.Properties["oauth_token_secret"];
                        App.User.TwitterId = eventArgs.Account.Properties["user_id"];
                        App.User.Name = eventArgs.Account.Properties["screen_name"];

                        //Store details for future use, 
                        //so we don't have to prompt authentication screen everytime
                        AccountStore.Create().Save(eventArgs.Account, "Twitter");

                        App.SuccessfulLoginAction.Invoke();
                    }
                };

                PresentViewController(auth.GetUI(), true, null);
            }
        }
    }
    //public partial class LoginPageRenderer : ContentPage
    //{
    //    public LoginPageRenderer()
    //    {
    //        InitializeComponent();
    //    }
    //}
}
