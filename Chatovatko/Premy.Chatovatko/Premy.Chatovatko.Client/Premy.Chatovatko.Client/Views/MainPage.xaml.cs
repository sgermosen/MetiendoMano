using Premy.Chatovatko.Client.Helpers;
using Premy.Chatovatko.Client.Libs.UserData;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Premy.Chatovatko.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        App app;
        public MainPage(App app, SettingsCapsula settings)
        {
            this.app = app;
            InitializeComponent();
            {
                var navigationPage = new NavigationPage(new ThreadsList(app, settings));
                navigationPage.Title = "Threads";
                Children.Add(navigationPage);
            }
            {
                var navigationPage = new NavigationPage(new ContactList(app, settings));
                navigationPage.Title = "Contacts";
                Children.Add(navigationPage);
            }
            
        }

        private void ChangeMainPage(Page page)
        {
            app.MainPage = page;
        }
    }
}
