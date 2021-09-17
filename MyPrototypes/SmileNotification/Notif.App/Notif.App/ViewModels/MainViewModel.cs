using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Notif.App.Views;
using Notif.Transversal.Models;
using Xamarin.Forms;

namespace Notif.App.ViewModels
{
    public class MainViewModel
    {
         

        public string UserEmail { get; set; }

        public string UserPassword { get; set; }
        
        private static MainViewModel instance;

        public TokenResponse Token { get; set; }

        public LoginViewModel Login { get; set; }

        public ReactionsViewModel Reactions { get; set; }
        
        public AddReactionViewModel AddReaction { get; set; }

        public ICommand AddReactionCommand => new RelayCommand(this.GoAddReaction);
            
        private async void GoAddReaction()
        {
            this.AddReaction = new AddReactionViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new AddReactionPage());
          //  await App.Navigator.PushAsync(new AddReactionPage());
        }


        public MainViewModel()
        {
            instance = this;
        }

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
    }
}
