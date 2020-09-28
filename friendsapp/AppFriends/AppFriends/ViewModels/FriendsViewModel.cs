using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AppFriends.Models;
using Xamarin.Forms;

namespace AppFriends.ViewModels
{
    public class FriendsViewModel :INotifyPropertyChanged
    {
        public Command GetFriendsCommand { get; set; }

        private bool Busy;
        public bool IsBusy
        {
            get { return Busy; }
            set
            {
                Busy = value;
                OnPropertyChanged();
                /**
                 * Note que estamos invocando o método OnPropertyChanged quando o valor da propriedade
                 * muda. A infraestrutura de ligação do Xamarin.Forms se inscreverá para nosso evento
                 * PropertyChanged para que a interface de usuário seja notificada da mudança.
                 */
                GetFriendsCommand.ChangeCanExecute();
            }
        }

        private string _completeName;
        public string CompleteName
        {
            get { return _completeName; }
            set
            {
                if (_completeName != null)
                {
                    _completeName = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Friend> Friends { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public FriendsViewModel()
        {
            Friends = new ObservableCollection<Friend>();

            GetFriendsCommand = new Command(
                async () => await GetFriends(),
                () => !IsBusy);
        }

        async Task GetFriends()
        {
            if (!IsBusy)
            {
                Exception error = null;
                try
                {
                    IsBusy = true;
                    var Repository = new Repository();
                    var Items = await Repository.GetFriends();

                    Friends.Clear();
                    foreach (var friend in Items)
                    {
                        Friends.Add(friend);
                    }
                }
                catch (Exception ex)
                {
                    error = ex;
                }
                finally
                {
                    IsBusy = false;
                }

                if (error != null)
                {
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Erro!", error.Message, "OK");
                }
            }
            return;
        }
    }
}

