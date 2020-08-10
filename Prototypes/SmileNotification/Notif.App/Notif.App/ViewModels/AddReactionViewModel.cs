using System;
using System.Collections.Generic;
using System.Text;

namespace Notif.App.ViewModels
{
    using System.Windows.Input;
    using Transversal.Models;
    using Transversal.Services;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;

    public class AddReactionViewModel : BaseViewModel
    {
        private bool isRunning;
        private bool isEnabled;
        private readonly ApiService apiService;

        //public string Image { get; set; }

        public bool IsRunning
        {
            get => this.isRunning;
            set => this.SetValue(ref this.isRunning, value);
        }

        public bool IsEnabled
        {
            get => this.isEnabled;
            set => this.SetValue(ref this.isEnabled, value);
        }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Observation { get; set; }

        public string Punctuation { get; set; }

        public ICommand SaveCommand => new RelayCommand(this.Save);

        public ICommand ImAngryCommand => new RelayCommand(this.ImAngry);

         
        private  void ImAngry()
        {
            this.Punctuation = 1.ToString();
        }

        public ICommand ImBadCommand => new RelayCommand(this.ImBad);


        private void ImBad()
        {
            this.Punctuation = 2.ToString();
        }

        public ICommand ImMehCommand => new RelayCommand(this.ImMeh);


        private void ImMeh()
        {
            this.Punctuation = 3.ToString();
        }

        public ICommand ImGoodCommand => new RelayCommand(this.ImGood);


        private void ImGood()
        {
            this.Punctuation = 4.ToString();
        }

        public ICommand ImLoveCommand => new RelayCommand(this.ImLove);


        private void ImLove()
        {
            this.Punctuation = 5.ToString();
        }

        public AddReactionViewModel()
        {
            this.apiService = new ApiService();
           // this.Image = "noImage";
           // this.Observation = "Observacion observable";
            //this.Name = "nombre";
            //this.Punctuation = "1";
            this.IsEnabled = true;
        }

        private async void Save()
        {
            //if (string.IsNullOrEmpty(this.Observation))
            //{
            //    await Application.Current.MainPage.DisplayAlert("Error", "You must enter a observation name.", "Accept");
            //    return;
            //}

            if (string.IsNullOrEmpty(this.Punctuation))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe escoger una reaccion", "Accept");
                return;
            }

            var punctuation = int.Parse(this.Punctuation);
            if (punctuation <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error",
                    "The punctuation must be a number greather than zero.", "Accept");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            if (string.IsNullOrEmpty(this.Email))
            {
                this.Email = "None";
            }
            if (string.IsNullOrEmpty(this.Name))
            {
                this.Name = "Cliente Generico";
            }
            if (string.IsNullOrEmpty(this.Observation))
            {
                this.Observation = " ";
            }

            //TODO: Add image
            var reaction = new ReactionResponse()
            {
                Observation = this.Observation,
                Punctuation = punctuation,
                Name = this.Name,
                User = new ApplicationUser
                {
                    UserName = MainViewModel.GetInstance().UserEmail,
                    Email = MainViewModel.GetInstance().UserEmail
                }
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.PostAsync(
                url,
                "/api",
                "/Reactions",
                reaction,
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            var newReaction = (ReactionResponse) response.Result;
            MainViewModel.GetInstance().Reactions.Reactions.Add(newReaction);

            this.IsRunning = false;
            this.IsEnabled = true;
          //  await App.Navigator.PopAsync();
        }

    }
}