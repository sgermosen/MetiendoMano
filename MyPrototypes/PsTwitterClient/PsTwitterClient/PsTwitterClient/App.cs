using PsTwitterClient.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace PsTwitterClient
{
    public class App : Application
    {
        static NavigationPage _NavigationPage;
        public static UserDetails User;
        public static MessageListPage _MessageListPage;

        public static Page GetMainPage()
        {
            _MessageList = _MessageList ?? new MessageListPage();
            _NavigationPage = new NavigationPage(_MessageListPage);
            return _NavigationPage;
        }

        public static Action SuccessfulLoginAction
        {
            get
            {
                return new Action(() =>
                {
                    _NavigationPage.Navigation.PushModalAsync(_MessageListPage.GetTimeline());
                });
            }
        }
        public class BaseContentPage : ContentPage
        {
            protected override void OnAppearing()
            {
                base.OnAppearing();

                if (App.User == null)
                {
                    Navigation.PushModalAsync(new LoginPage());
                }
            }
        }
        public class MessageListPage : BaseContentPage
        {
            public MessageListPage()
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children =
            {
                new Label
                {
                    XAlign = TextAlignment.Center,
                    Text = "Welcome to Xamarin!!"
                }
            }
                };
            }
        }
        //public App()
        //{
        //    // The root page of your application
        //    var content = new ContentPage
        //    {
        //        Title = "PsTwitterClient",
        //        Content = new StackLayout
        //        {
        //            VerticalOptions = LayoutOptions.Center,
        //            Children = {
        //                new Label {
        //                    HorizontalTextAlignment = TextAlignment.Center,
        //                    Text = "Welcome to Xamarin Forms!"
        //                }
        //            }
        //        }
        //    };

        //    MainPage = new NavigationPage(content);
        //}

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
