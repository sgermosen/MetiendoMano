using System.Linq;

namespace Notif.App.ViewModels
{
    using Transversal.Models;
    using Transversal.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    public class ReactionsViewModel: BaseViewModel
    {
        private readonly ApiService _apiService;
        private ObservableCollection<ReactionResponse> _reactions;
        private bool _isRefreshing;

        private List<ReactionResponse> myReactions;
        //private ObservableCollection<ReactionItemViewModel> reactions;
         
        public ObservableCollection<ReactionResponse> Reactions
        {
            get => this._reactions;
            set => this.SetValue(ref this._reactions, value);
        }

        public bool IsRefreshing
        {
            get => this._isRefreshing;
            set => this.SetValue(ref this._isRefreshing, value);
        }

        public ReactionsViewModel()
        {
            this._apiService = new ApiService();
          this.LoadReactions();
        }

        private async void LoadReactions()
        {
            this.IsRefreshing = true;

            //var response = await this._apiService.GetListAsync<ReactionResponse>(
            //    "https://reactions911.azurewebsites.net",
            //    "/api",
            //    "/Reactions");
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await _apiService.GetListAsync<ReactionResponse>(
                url,
                "/api",
                "/Reactions",
                "bearer",
                MainViewModel.GetInstance().Token.Token);


            //this.IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                this.IsRefreshing = false;
                return;
            }

            //var myReactions = (List<ReactionResponse>)response.Result;
            //this.Reactions = new ObservableCollection<ReactionResponse>(myReactions.OrderBy(p => p.Name));
            //var products = (List<Product>)response.Result;
            //this.Products = new ObservableCollection<Product>(products);
       

            //this.Reactions = new ObservableCollection<ReactionResponse>(myReactions);
            this.myReactions = (List<ReactionResponse>)response.Result;
            this.RefresReactionsList();
            this.IsRefreshing = false;
        }

        public void AddReactionToList(ReactionResponse reaction)
        {
            this.myReactions.Add(reaction);
            this.RefresReactionsList();
        }

        public void UpdateReactionInList(ReactionResponse reaction)
        {
            var previousReaction = myReactions.FirstOrDefault(p => p.Id == reaction.Id);
            if (previousReaction != null)
            {
                this.myReactions.Remove(previousReaction);
            }

            this.myReactions.Add(reaction);
            this.RefresReactionsList();
        }

        public void DeleteReactionInList(int reactionId)
        {
            var previousReaction = this.myReactions.FirstOrDefault(p => p.Id == reactionId);
            if (previousReaction != null)
            {
                this.myReactions.Remove(previousReaction);
            }

            this.RefresReactionsList();
        }

        private void RefresReactionsList()
        {
            this.Reactions = new ObservableCollection<ReactionResponse>(myReactions.Select(p => new ReactionItemViewModel
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name,
                    Punctuation = p.Punctuation,
                    Email = p.Email,
                    User = p.User
                })
                .OrderBy(p => p.Name)
                .ToList());
        }

    }
}
 
