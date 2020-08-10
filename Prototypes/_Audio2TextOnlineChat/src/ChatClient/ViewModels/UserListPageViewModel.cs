using Ardalis.GuardClauses;
using ChatClient.Services;
using ChatClient.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Web.Core.Data;
using Xamarin.Forms;

namespace ChatClient.ViewModels
{
    public class UserListPageViewModel : ViewModelBase
    {
        #region Fields

        private readonly IPageDialogService _dialogService;
        private readonly AudioPlayer _audioPlayer;
        private readonly ClientChatHub _chatHub;
        private UserViewModel _selectedUser;

        #endregion

        public UserListPageViewModel(INavigationService navigationService,
            IPageDialogService dialogService,
            AudioPlayer audioPlayer,
            ClientChatHub chatHub)
            : base(navigationService)
        {
            Guard.Against.Null(dialogService, nameof(dialogService));
            Guard.Against.Null(audioPlayer, nameof(audioPlayer));
            Guard.Against.Null(chatHub, nameof(chatHub));

            _dialogService = dialogService;
            _audioPlayer = audioPlayer;
            _chatHub = chatHub;

            _chatHub.OnUpdateConnectedUsers(UpdateConnectedUser);
            _chatHub.OnNewUserAvailable(NewUserAvailable);
            _chatHub.OnUserNotAvailable(UserNotAvailable);
            _chatHub.OnUpdateUnreadMessages(UpdateUnreadMessages);

            Title = Resources.MainPageTitle;
            IsProcessing = false;
            AvailableUsers = new ObservableCollection<UserViewModel>();
            UserSelectedCommand = new DelegateCommand<UserViewModel>(UserSelected);

            RequestAvailableUsers();
        }

        #region Properties

        public UserViewModel User { get; private set; }
        public int? UsersCount => AvailableUsers?.Count;
        public ICommand UserSelectedCommand { get; }
        public ObservableCollection<UserViewModel> AvailableUsers { get; }
        public bool IsProcessing { get; set; }

        #endregion

        #region Commands

        private async void RequestAvailableUsers()
        {
            try
            {
                await _chatHub.RequestAvailableUsers();
            }
            catch (Exception)
            {
                await _dialogService.DisplayAlertAsync("Error", Resources.UnableToGetConnectedUsers, "Ok");
            }
        }

        private async void UserSelected(UserViewModel user)
        {
            _selectedUser = user;

            try
            {
                Device.BeginInvokeOnMainThread(() => IsProcessing = true);

                await NavigationService.NavigateAsync(nameof(ChatPage), new NavigationParameters
                {
                    { nameof(ChatPageViewModel.FromUser), User },
                    { nameof(ChatPageViewModel.ToUser), user }
                });

                Device.BeginInvokeOnMainThread(() => IsProcessing = false);
            }
            catch (Exception)
            {
                await _dialogService.DisplayAlertAsync("Error", Resources.UserIsDisconnected, "Ok");
                Device.BeginInvokeOnMainThread(() => IsProcessing = false);
            }

            Device.BeginInvokeOnMainThread(() =>
            {
                if (user.MessageCount > 0)
                    user.MessageCount = 0;
            });
        }

        #endregion

        #region Events

        private void UpdateConnectedUser(List<UserViewModel> connectedUsers)
        {
            Guard.Against.Null(connectedUsers, nameof(connectedUsers));

            Device.BeginInvokeOnMainThread(() =>
            {
                IsProcessing = true;
                foreach (var user in connectedUsers)
                {
                    user.UserSelectedCommand = UserSelectedCommand;

                    AvailableUsers.Add(user);
                }
                RaisePropertyChanged(nameof(UsersCount));
                IsProcessing = false;
            });
        }

        private void UpdateUnreadMessages(Guid userId)
        {
            Guard.Against.Default(userId, nameof(userId));

            var user = AvailableUsers.FirstOrDefault(x => x.UserId == userId);
            if (user is null || user.Equals(_selectedUser))
                return;

            Device.BeginInvokeOnMainThread(() =>
            {
                user.MessageCount++;
            });

            _audioPlayer.PlayNotificationSound();
        }

        private void UserNotAvailable(Guid userId)
        {
            Guard.Against.Default(userId, nameof(userId));

            var user = AvailableUsers.FirstOrDefault(x => x.UserId == userId);
            if (user is null)
                return;

            Device.BeginInvokeOnMainThread(() =>
            {
                AvailableUsers.Remove(user);
                RaisePropertyChanged(nameof(UsersCount));
            });
        }

        private void NewUserAvailable(UserViewModel user)
        {
            Guard.Against.Null(user, nameof(user));

            user.UserSelectedCommand = UserSelectedCommand;

            Device.BeginInvokeOnMainThread(() =>
            {
                AvailableUsers.Add(user);
                RaisePropertyChanged(nameof(UsersCount));
            });
        }

        #endregion

        #region Navigation Methods

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            _selectedUser = null;

            if (!parameters.Any())
                return;

            User = parameters.GetValue<UserViewModel>(nameof(UserViewModel));
        }

        #endregion
    }
}
