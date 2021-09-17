using Ardalis.GuardClauses;
using ChatClient.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Web.Core.Data;
using Web.Core.Models;
using Xamarin.Forms;

namespace ChatClient.ViewModels
{
    public class ChatPageViewModel : ViewModelBase
    {
        private readonly IPageDialogService _dialogService;
        private readonly ClientChatHub _chatHub;

        public ChatPageViewModel(INavigationService navigationService,
            IPageDialogService dialogService,
            ClientChatHub chatHub)
            : base(navigationService)
        {
            Guard.Against.Null(dialogService, nameof(dialogService));
            Guard.Against.Null(chatHub, nameof(chatHub));

            _dialogService = dialogService;
            _chatHub = chatHub;

            _chatHub.OnConnectWith(ConnectWith);
            _chatHub.OnReceiveConversation(ReceiveConversation);
            _chatHub.OnReceiveMessage(ReceiveMessage);

            Message = "";
            IsProcessing = false;
            Messages = new ObservableCollection<MessageDetail>();
            SendMessageCommand = new DelegateCommand(SendMessage)
                .ObservesCanExecute(() => IsMessageValid);
        }

        #region Properties

        public UserViewModel ToUser { get; set; }
        public UserViewModel FromUser { get; set; }
        public string Message { get; set; }
        public int? MessagesCount => Messages?.Count;
        public bool IsMessageValid => !(string.IsNullOrWhiteSpace(Message) || Message.Length > 500);
        public ObservableCollection<MessageDetail> Messages { get; }
        public DelegateCommand SendMessageCommand { get; }
        public bool IsProcessing { get; set; }

        #endregion

        #region Commands

        private async void SendMessage()
        {
            var message = new MessageDetail
            {
                FromUserId = FromUser.UserId,
                FromUserName = FromUser.UserName,
                ToUserId = ToUser.UserId,
                MessageDate = DateTime.Now,
                Message = Message
            };

            try
            {
                await _chatHub.SendMessage(message);

                Device.BeginInvokeOnMainThread(() =>
                {
                    IsProcessing = true;
                    message.FromUserName = "Me";
                    Messages.Add(message);
                    RaisePropertyChanged(nameof(MessagesCount));
                    Message = "";
                    IsProcessing = false;
                });
            }
            catch (Exception)
            {
                await _dialogService.DisplayAlertAsync("Error", Resources.UnableToSendTheMessage, "Ok");
            }
        }

        private async void RequestConversation()
        {
            try
            {
                await _chatHub.RequestConversation(FromUser.UserId, ToUser.UserId);
            }
            catch (Exception)
            {
                await _dialogService.DisplayAlertAsync("Error", Resources.UnableToGetMessages, "Ok");
            }
        }

        #endregion

        #region Events

        private void ConnectWith(Guid toUserId)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ToUser = new UserViewModel
                {
                    UserName = "admin",
                    UserId = toUserId
                };
                Title = string.Format(Resources.ChatPageTitle, ToUser.UserName);
            });
        }

        private void ReceiveConversation(List<MessageDetail> messages)
        {
            Guard.Against.Null(messages, nameof(messages));

            Device.BeginInvokeOnMainThread(() =>
            {
                IsProcessing = true;
                foreach (var message in messages)
                {
                    if (message.FromUserName == FromUser.UserName)
                        message.FromUserName = "Me";

                    Messages.Add(message);
                }
                RaisePropertyChanged(nameof(MessagesCount));
                IsProcessing = false;
            });
        }

        private void ReceiveMessage(MessageDetail message)
        {
            Guard.Against.Null(message, nameof(message));

            if (message.FromUserId != ToUser.UserId)
                return;

            Device.BeginInvokeOnMainThread(() =>
            {
                Messages.Add(message);
                RaisePropertyChanged(nameof(MessagesCount));
            });
        }

        #endregion

        #region Navigation Methods

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (!parameters.Any())
                return;

            FromUser = parameters.GetValue<UserViewModel>(nameof(FromUser));
            ToUser = parameters.GetValue<UserViewModel>(nameof(ToUser));

            if (ToUser != null)
            {
                RequestConversation();
                Device.BeginInvokeOnMainThread(() =>
                {
                    Title = string.Format(Resources.ChatPageTitle, ToUser.UserName);
                });
                return;
            }

            ToUser = new UserViewModel();
            Device.BeginInvokeOnMainThread(() =>
            {
                Messages.Add(new MessageDetail
                {
                    Message = Resources.InitialMessageTest
                });
                Title = Resources.ChatPageTitleOnHold;
                RaisePropertyChanged(nameof(MessagesCount));
            });
        }

        #endregion
    }
}
