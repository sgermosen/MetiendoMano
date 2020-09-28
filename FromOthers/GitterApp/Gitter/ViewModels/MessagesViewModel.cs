using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Akavache;
using Gitter.Models;
using ReactiveUI;
using Splat;

namespace Gitter.ViewModels
{
    public class MessagesViewModel : ReactiveObject, IRoutableViewModel
    {
        private string messageText;

        public MessagesViewModel(Room room, IGitterApi api = null, IScreen hostScreen = null)
        {
            this.HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();

            this.Messages = new ReactiveList<MessageViewModel>();
            this.UrlPathSegment = room.name;

            IObservable<string> accessToken = GitterApi.GetAccessToken().PublishLast().RefCount();

            this.LoadMessages = ReactiveCommand.CreateAsyncObservable(_ => accessToken.SelectMany(token => (api ?? GitterApi.UserInitiated).GetMessages(room.id, token)));

            this.Users = new ReactiveList<UserViewModel>();
            this.LoadUsers = ReactiveCommand.CreateAsyncObservable(_ => accessToken
                .SelectMany(token => (api ?? GitterApi.UserInitiated).GetRoomUsers(room.id, token))
                .Select(users => users.OrderBy(user => user.displayName, StringComparer.CurrentCulture).Select(user => new UserViewModel(user))));
            this.LoadUsers.Subscribe(x =>
            {
                using (this.Users.SuppressChangeNotifications())
                {
                    this.Users.Clear();
                    this.Users.AddRange(x);
                }
            });

            this.SendMessage = ReactiveCommand.CreateAsyncTask(this.WhenAnyValue(x => x.MessageText, x => !String.IsNullOrWhiteSpace(x)), async _ =>
            {
                await (api ?? GitterApi.UserInitiated).SendMessage(room.id, new SendMessage(this.MessageText), await accessToken);
                this.MessageText = String.Empty;
            });

            this.LoadMessages.FirstAsync().SelectMany(x => x.ToObservable())
                .Concat(this.StreamMessages(room.id))
                .Select(x => new MessageViewModel(x))
                .Subscribe(x =>
                {
                    this.Messages.Insert(0, x);
                });
        }

        public IScreen HostScreen { get; private set; }

        public ReactiveCommand<IReadOnlyList<Message>> LoadMessages { get; private set; }

        public IReactiveList<MessageViewModel> Messages { get; private set; }

        public ReactiveCommand<IEnumerable<UserViewModel>> LoadUsers { get; private set; }

        public IReactiveList<UserViewModel> Users { get; private set; }

        public string MessageText
        {
            get { return this.messageText; }
            set { this.RaiseAndSetIfChanged(ref this.messageText, value); }
        }

        public ReactiveCommand<Unit> SendMessage { get; private set; }

        public string UrlPathSegment { get; private set; }

        private IObservable<Message> StreamMessages(string roomId)
        {
            var streamApi = new GitterStreamingApi();

            return GitterApi.GetAccessToken()
                .SelectMany(x => streamApi.ObserveMessages(roomId, x));
        }
    }
}