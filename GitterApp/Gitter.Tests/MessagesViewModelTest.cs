using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using Gitter.Models;
using Gitter.ViewModels;
using NSubstitute;
using NUnit.Framework;
using ReactiveUI;

namespace Gitter.Tests
{
    public class MessagesViewModelTest
    {
        [TestFixture]
        public class TheSendMessageCommand
        {
            [Test]
            public async Task CanExecuteWhenMessageIsNotNullOrEmpty()
            {
                await BlobCache.Secure.SaveLogin("Gitter", "TheAccessToken", "Gitter");
                var room = new Room { name = "TheRoom", id = "TheRoomId" };
                var api = Substitute.For<IGitterApi>();
                api.GetMessages(Arg.Any<string>(), Arg.Any<string>()).Returns(Observable.Return((IReadOnlyList<Message>)new List<Message>()));
                var fixture = new MessagesViewModel(room, api, Substitute.For<IScreen>());
                await fixture.LoadMessages.ExecuteAsync();

                fixture.MessageText = "TheMessage";
                Assert.True(fixture.SendMessage.CanExecute(null));

                fixture.MessageText = null;
                Assert.False(fixture.SendMessage.CanExecute(null));

                fixture.MessageText = String.Empty;
                Assert.False(fixture.SendMessage.CanExecute(null));

                fixture.MessageText = " ";
                Assert.False(fixture.SendMessage.CanExecute(null));
            }

            [Test]
            public async Task ClearsMessageTextAfterSending()
            {
                await BlobCache.Secure.SaveLogin("Gitter", "TheAccessToken", "Gitter");
                var room = new Room { name = "TheRoom", id = "TheRoomId" };
                var api = Substitute.For<IGitterApi>();
                api.GetMessages(Arg.Any<string>(), Arg.Any<string>()).Returns(Observable.Return((IReadOnlyList<Message>)new List<Message>()));
                var fixture = new MessagesViewModel(room, api, Substitute.For<IScreen>());
                await fixture.LoadMessages.ExecuteAsync();

                fixture.MessageText = "TheMessage";

                await fixture.SendMessage.ExecuteAsync();

                Assert.IsEmpty(fixture.MessageText);
            }

            [Test]
            public async Task SmokeTest()
            {
                await BlobCache.Secure.SaveLogin("Gitter", "TheAccessToken", "Gitter");
                var room = new Room { name = "TheRoom", id = "TheRoomId" };
                var api = Substitute.For<IGitterApi>();
                api.GetMessages(Arg.Any<string>(), Arg.Any<string>()).Returns(Observable.Return((IReadOnlyList<Message>)new List<Message>()));
                var fixture = new MessagesViewModel(room, api, Substitute.For<IScreen>());
                await fixture.LoadMessages.ExecuteAsync();

                fixture.MessageText = "TheMessage";

                await fixture.SendMessage.ExecuteAsync();

                api.Received(1).SendMessage(room.id, Arg.Is<SendMessage>(x => x.Text == "TheMessage"), "Bearer TheAccessToken");
            }
        }
    }
}