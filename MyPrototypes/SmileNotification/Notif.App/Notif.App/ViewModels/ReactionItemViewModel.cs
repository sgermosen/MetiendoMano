using System;
using System.Collections.Generic;
using System.Text;
using Notif.Transversal.Models;

namespace Notif.App.ViewModels
{
    using System.Windows.Input; 
    using GalaSoft.MvvmLight.Command;
    using Views;

   public class ReactionItemViewModel:ReactionResponse
    {
        //public ICommand SelectReactionCommand => new RelayCommand(this.SelectReaction);

        //private async void SelectReaction()
        //{
        //    MainViewModel.GetInstance().EditReaction = new EditReactionViewModel(this);
        //    await App.Navigator.PushAsync(new EditReactionPage());
        //}

    }
}
