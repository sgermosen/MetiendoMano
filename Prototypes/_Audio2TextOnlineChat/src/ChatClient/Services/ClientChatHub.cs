using ChatClient.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Web.Core.Data;
using Web.Core.Models;

namespace ChatClient.Services
{
    public class ClientChatHub
    {
        private readonly HubConnection _connection;
        private Task _lastProses;

        public ClientChatHub()
        {
            var apiUrl = EnvironmentVariables.ChatHubUrl;

            _lastProses = null;
            _connection = new HubConnectionBuilder()
                .WithUrl(apiUrl, options =>
                {
                    options.HttpMessageHandlerFactory = handler =>
                    {
                        // to enable ssl certification on production add Debugger.IsAttached
                        // property on the condition.
                        //if (Debugger.IsAttached && handler is HttpClientHandler clientHandler)
                        if (handler is HttpClientHandler clientHandler)
                        {
                            clientHandler.ServerCertificateCustomValidationCallback = ValidateCertificate;
                        }
                        return handler;
                    };
                })
                .Build();

            var reconnectionCount = 0;
            _connection.Closed += async error =>
            {
                if (_lastProses != null)
                    Task.WaitAll(_lastProses);

                if (reconnectionCount++ > 9)
                    return;

                await Task.Delay(1000);
                await _connection.StartAsync();
            };
        }

        #region Properties

        public bool IsConnected => !string.IsNullOrEmpty(_connection?.ConnectionId);

        #endregion

        #region ConnectPage Methods

        public Task Connect()
        {
            if (IsConnected)
                throw new InvalidOperationException(Resources.UserIsConnected);

            if (_lastProses != null)
                Task.WaitAll(_lastProses);

            return (_lastProses = _connection.StartAsync());
        }

        #endregion

        #region UserListPage Methods

        public Task RequestAvailableUsers() =>
            _connection.InvokeAsync(ChatHubMethods.RequestConnectedUsers);

        public Task<object> Connect(string userName, bool isNewRoom = false) =>
            _connection.InvokeAsync<object>(ChatHubMethods.Connect, userName, isNewRoom);

        public Task Disconnect()
        {
            if (_lastProses != null)
                Task.WaitAll(_lastProses);

            return (_lastProses = _connection.StopAsync());
        }

        public void OnUpdateConnectedUsers(Action<List<UserViewModel>> action) =>
            _connection.On(ChatHubMethods.UpdateConnectedUsers, action);

        public void OnNewUserAvailable(Action<UserViewModel> action) =>
            _connection.On(ChatHubMethods.NewUserAvailable, action);

        public void OnUserNotAvailable(Action<Guid> action) =>
            _connection.On(ChatHubMethods.UserNotAvailable, action);

        public void OnUpdateUnreadMessages(Action<Guid> action) =>
            _connection.On(ChatHubMethods.UpdateUnreadMessages, action);

        #endregion

        #region ChatPage Methods

        public Task RequestConversation(Guid fromUserId, Guid toUserId) =>
            _connection.InvokeAsync(ChatHubMethods.RequestConversation, fromUserId, toUserId);

        public Task SendMessage(MessageDetail message)
        {
            string methodName = Guid.Empty.Equals(message.ToUserId) ?
                ChatHubMethods.CacheMessage : ChatHubMethods.SendPrivateMessage;

            return _connection.InvokeAsync(methodName, message);
        }

        public void OnConnectWith(Action<Guid> action) =>
            _connection.On(ChatHubMethods.ConnectWith, action);

        public void OnReceiveConversation(Action<List<MessageDetail>> action) =>
            _connection.On(ChatHubMethods.ReceiveConversation, action);

        public void OnReceiveMessage(Action<MessageDetail> action) =>
            _connection.On(ChatHubMethods.ReceiveMessage, action);

        #endregion

        #region Auxiliaxy Methods

        private bool ValidateCertificate(HttpRequestMessage arg1,
            X509Certificate2 arg2,
            X509Chain arg3,
            SslPolicyErrors arg4)
        {
            // ignore ssl certificate validation on debugging
            return true;
        }

        #endregion
    }
}
