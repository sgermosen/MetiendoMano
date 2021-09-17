using Ardalis.GuardClauses;
using ChatClient.Services;
using ChatClient.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Text.Json;
using System.Windows.Input;
using Web.Core.Data;
using Xamarin.Forms;

namespace ChatClient.ViewModels
{
    public class ConnectPageViewModel : ViewModelBase
    {
        private readonly IPageDialogService _dialogService;
        private readonly ClientChatHub _chatHub;


        public ConnectPageViewModel(INavigationService navigationService,
            IPageDialogService dialogService,
            ClientChatHub chatHub)
            : base(navigationService)
        {
            Guard.Against.Null(dialogService, nameof(dialogService));
            Guard.Against.Null(chatHub, nameof(chatHub));

            _dialogService = dialogService;
            _chatHub = chatHub;

            Title = Resources.ConnectPageTitle;
            UserName = "";
            IsProcessing = false;
            ConnectAsUserCommand = new DelegateCommand(ConnectAsUser)
                .ObservesCanExecute(() => IsUserNameValid);
            ConnectAsAdminCommand = new DelegateCommand(ConnectAsAdmin)
                .ObservesCanExecute(() => IsUserNameValid);
            //ValidateUserNameCommand = new DelegateCommand(ValidateUserName);
        }

        #region Properties

        public string UserName { get; set; }
        public bool IsProcessing { get; set; }
        private bool IsUserNameValid => !(string.IsNullOrWhiteSpace(UserName) || UserName.Length > 10);
        public ICommand ConnectAsUserCommand { get; }
        public ICommand ConnectAsAdminCommand { get; }

        // validate user input can be called on input or connect command
        //public ICommand ValidateUserNameCommand { get; }

        #endregion

        #region Commands

        private async void ConnectAsUser()
        {
            if (!IsUserNameValid)
                return;
            try
            {
                Device.BeginInvokeOnMainThread(() => IsProcessing = true);

                if (!_chatHub.IsConnected)
                    await _chatHub.Connect();

                var result = await _chatHub.Connect(UserName, true);
                var userId = ((JsonElement)result).GetProperty("value").GetGuid();

                var user = new UserViewModel
                {
                    UserName = UserName,
                    UserId = userId
                };

                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(ChatPage)}",
                    new NavigationParameters
                    {
                        { nameof(ChatPageViewModel.FromUser), user },
                        { nameof(ChatPageViewModel.ToUser), null }
                    });

                Device.BeginInvokeOnMainThread(() => IsProcessing = false);
            }
            catch (Exception)
            {
                await _dialogService.DisplayAlertAsync("Error", Resources.UserIsConnected, "Ok");
                Device.BeginInvokeOnMainThread(() => IsProcessing = false);
            }
        }

        private async void ConnectAsAdmin()
        {
            if (!IsUserNameValid)
                return;

            try
            {
                Device.BeginInvokeOnMainThread(() => IsProcessing = true);

                if (!_chatHub.IsConnected)
                    await _chatHub.Connect();

                var result = await _chatHub.Connect(UserName);
                var userId = ((JsonElement)result).GetProperty("value").GetGuid();

                var user = new UserViewModel
                {
                    UserName = UserName,
                    UserId = userId,
                    IsAdmin = true
                };

                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(UserListPage)}",
                    new NavigationParameters
                    {
                        { nameof(UserViewModel), user }
                    });

                Device.BeginInvokeOnMainThread(() => IsProcessing = false);
            }
            catch (Exception)
            {
                await _dialogService.DisplayAlertAsync("Error", Resources.UserIsConnected, "Ok");
                Device.BeginInvokeOnMainThread(() => IsProcessing = false);
            }
        }

        //private void ValidateUserName()
        //{
        //    Console.WriteLine(UserName);
        //}

        #endregion
    }
}
