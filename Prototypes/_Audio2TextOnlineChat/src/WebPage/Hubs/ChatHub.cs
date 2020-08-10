using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Core.Data;
using Web.Core.Models;

namespace WebPage.Hubs
{
    public class ChatHub : Hub
    {
        #region Fields And Properties

        private static readonly object Locker = new object();

        private static List<UserDetail> _connectedUsers;
        public static List<UserDetail> ConnectedUsers
        {
            get
            {
                lock (Locker)
                {
                    return _connectedUsers ??= new List<UserDetail>();
                }
            }
        }

        private static List<MessageDetail> _currentMessage;
        public static List<MessageDetail> CurrentMessages
        {
            get
            {
                lock (Locker)
                {
                    return _currentMessage ??= new List<MessageDetail>();
                }
            }
        }

        #endregion

        #region Connect Methods

        public async Task<IActionResult> Connect(string userName, bool isHolding)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrWhiteSpace(userName))
                throw new InvalidDataException(Resources.InvalidUserName);

            if (ConnectedUsers.Any(x => x.ConnectionId.Equals(Context.ConnectionId)))
                throw new InvalidOperationException(Resources.UserIsConnected);

            var user = new UserDetail
            {
                UserId = Guid.NewGuid(),
                ConnectionId = Context.ConnectionId,
                UserName = userName,
                IsOnHold = isHolding
            };

            ConnectedUsers.Add(user);

            if (user.IsOnHold)
            {
                // send to all except caller client
                await Clients.AllExcept(user.ConnectionId)
                    .SendAsync(ChatHubMethods.NewUserAvailable, new
                    {
                        userId = user.UserId,
                        userName = user.UserName,
                        messageCount = 0
                    });
            }

            return new OkObjectResult(user.UserId);
        }

        public async void RequestConnectedUsers()
        {
            // send to caller user
            var availableUser = ConnectedUsers.Where(x => x.IsOnHold)
                .Select(x => new
                {
                    userId = x.UserId,
                    userName = x.UserName,
                    messageCount = CurrentMessages.Count(y => y.FromUserId == x.UserId)
                });
            await Clients.Caller.SendAsync(ChatHubMethods.UpdateConnectedUsers, availableUser);
        }

        #endregion

        #region Message Methods

        public async Task<IActionResult> CacheMessage(MessageDetail message)
        {
            if (!ConnectedUsers.Any(x => x.UserId.Equals(message.FromUserId)))
                throw new InvalidOperationException(Resources.UserIsDisconnected);

            // update notification indicator
            await Clients.AllExcept(Context.ConnectionId)
                .SendAsync(ChatHubMethods.UpdateUnreadMessages, message.FromUserId);

            AddMessageInCache(message);

            return new OkResult();
        }

        public async Task<IActionResult> SendPrivateMessage(MessageDetail message)
        {
            if (!ConnectedUsers.Any(x => x.UserId.Equals(message.FromUserId)))
                throw new InvalidOperationException(Resources.UserIsDisconnected);

            var toUser = ConnectedUsers.FirstOrDefault(x => x.UserId == message.ToUserId);
            if (toUser is null)
                throw new InvalidOperationException(Resources.UserIsDisconnected);

            await Clients.AllExcept(Context.ConnectionId)
                .SendAsync(ChatHubMethods.UpdateUnreadMessages, message.FromUserId);

            // send a private message to listener user
            await Clients.Client(toUser.ConnectionId)
                .SendAsync(ChatHubMethods.ReceiveMessage, message);

            AddMessageInCache(message);

            return new OkResult();
        }

        public async Task<IActionResult> RequestConversation(string fromId, string toId)
        {
            Guid fromUserId = Guid.Parse(fromId);
            Guid toUserId = Guid.Parse(toId);

            if (!ConnectedUsers.Any(x => x.UserId.Equals(fromUserId)))
                throw new InvalidOperationException(Resources.UserIsDisconnected);

            var toUser = ConnectedUsers.FirstOrDefault(x =>
                x.UserId == toUserId &&
                (x.IsOnHold || x.ConnectedWith == fromUserId));
            if (toUser is null)
                throw new InvalidOperationException(Resources.UserIsDisconnected);

            if (toUser.IsOnHold)
            {
                toUser.IsOnHold = false;
                toUser.ConnectedWith = fromUserId;
                // set all cached messages to the requesting user
                CurrentMessages.Where(u => u.FromUserId == toUserId)
                    .ToList()
                    .ForEach(message => message.ToUserId = fromUserId);
            }

            // get all cached messages
            var cachedConversation = CurrentMessages
                .Where(u =>
                    (u.FromUserId == fromUserId && u.ToUserId == toUserId) ||
                    (u.FromUserId == toUserId && u.ToUserId == fromUserId));

            // hide user to others
            await Clients.AllExcept(Context.ConnectionId)
                .SendAsync(ChatHubMethods.UserNotAvailable, toUserId);

            await Clients.Client(toUser.ConnectionId)
                .SendAsync(ChatHubMethods.ConnectWith, fromUserId);

            await Clients.Caller.SendAsync(ChatHubMethods.ReceiveConversation, cachedConversation);

            return new OkResult();
        }

        #endregion

        #region Overridden Methods

        public override Task OnConnectedAsync()
        {
            var user = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (user == null)
                return base.OnConnectedAsync();

            Clients.All.SendAsync(ChatHubMethods.UserNotAvailable, user.UserId);
            ConnectedUsers.Remove(user);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var user = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (user == null)
                return base.OnDisconnectedAsync(exception);

            Clients.All.SendAsync(ChatHubMethods.UserNotAvailable, user.UserId);
            ConnectedUsers.Remove(user);
            return base.OnDisconnectedAsync(exception);
        }

        #endregion

        #region Auxiliary Methods

        private static void AddMessageInCache(MessageDetail _MessageDetail)
        {
            CurrentMessages.Add(_MessageDetail);
            if (CurrentMessages.Count > 1000)
                CurrentMessages.RemoveAt(0);
        }

        #endregion
    }
}