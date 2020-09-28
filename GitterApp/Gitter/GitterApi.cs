using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akavache;
using Fusillade;
using Gitter.Models;
using ModernHttpClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Refit;

namespace Gitter
{
    [Headers("Accept: application/json")]
    public interface IGitterApi
    {
        [Get("/rooms/{id}/chatMessages")]
        IObservable<IReadOnlyList<Message>> GetMessages([AliasAs("id")] string roomId, [Header("Authorization")] string accessToken);

        [Get("/rooms/{id}/users")]
        IObservable<IReadOnlyList<User>> GetRoomUsers([AliasAs("id")] string roomId, [Header("Authorization")] string accessToken);

        [Get("/rooms")]
        Task<IReadOnlyList<Room>> GetRooms([Header("Authorization")] string accessToken);

        [Post("/rooms/{id}/chatMessages")]
        Task<Unit> SendMessage([AliasAs("id")] string roomId, [Body] SendMessage message, [Header("Authorization")] string accessToken);
    }

    public interface IGitterStreamingApi
    {
        IObservable<Message> ObserveMessages(string roomId, string accessToken);
    }

    public class GitterApi
    {
        public static readonly string ApiBaseAddress = "https://api.gitter.im/v1";

        private static readonly Lazy<IGitterApi> userInitiated;

        static GitterApi()
        {
            userInitiated = new Lazy<IGitterApi>(() =>
            {
                var client = new HttpClient(NetCache.UserInitiated)
                {
                    BaseAddress = new Uri(ApiBaseAddress)
                };

                return RestService.For<IGitterApi>(client);
            });
        }

        public static IGitterApi UserInitiated
        {
            get { return userInitiated.Value; }
        }

        /// <summary>
        /// Gets the formatted access token ready o be passed directly into the REST API.
        /// </summary>
        /// <exception cref="KeyNotFoundException">The access token isn't stored.</exception>
        public static IObservable<string> GetAccessToken()
        {
            return Observable.Defer(() => BlobCache.Secure.GetLoginAsync("Gitter"))
                .Select(x => "Bearer " + x.Password);
        }
    }

    public class GitterStreamingApi : IGitterStreamingApi
    {
        public IObservable<Message> ObserveMessages(string roomId, string accessToken)
        {
            string url = string.Format("https://stream.gitter.im/v1/rooms/{0}/chatMessages", roomId);

            return Observable.Using(() =>
            {
                var client = new HttpClient(new NativeMessageHandler());
                client.DefaultRequestHeaders.Add("Authorization", accessToken);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                return client;
            }, client => client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).ToObservable()
                .SelectMany(x => x.Content.ReadAsStreamAsync())
                .Select(x => Observable.FromAsync(() => ReadLine(x)).Repeat())
                .Concat()
                .Where(x => !String.IsNullOrWhiteSpace(x))
                .Select(x => JObject.Parse(x).ToObject<Message>()));
        }

        private async Task<string> ReadLine(Stream stream)
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8, false, 1024, true))
            {
                string line = await reader.ReadLineAsync();

                return line;
            }
        }
    }
}