using LineDietXF.Extensions;
using LineDietXF.Interfaces;
using Plugin.Share;
using Plugin.Share.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace LineDietXF.ViewModels
{
    public class AboutPageViewModel : BaseViewModel, INavigationAware
    {
        // Bindable Commands
        public DelegateCommand CloseCommand { get; set; }
        public DelegateCommand VisitSmartyPNetCommand { get; set; }
        public DelegateCommand VisitSPCWebsiteCommand { get; set; }
        public DelegateCommand VisitGithubCommand { get; set; }

        public AboutPageViewModel(INavigationService navigationService, ISettingsService settingsService, IAnalyticsService analyticsService, IPageDialogService dialogService) :
            base(navigationService, settingsService, analyticsService, dialogService)
        {
            // Wire up commands
            VisitSmartyPNetCommand = new DelegateCommand(VisitSmartyPNet);
            VisitSPCWebsiteCommand = new DelegateCommand(VisitSPCWebsite);
            VisitGithubCommand = new DelegateCommand(VisitGithub);
            CloseCommand = new DelegateCommand(Close);
        }

        public void OnNavigatingTo(NavigationParameters parameters) { }
        public void OnNavigatedFrom(NavigationParameters parameters) { }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            AnalyticsService.TrackPageView(Constants.Analytics.Page_About);
        }

        async void VisitWebsite(string url)
        {
            await CrossShare.Current.OpenBrowser(url,
                new BrowserOptions()
                {
                    SafariBarTintColor = Constants.UI.WebBrowserNavColor.ToShareColor(),
                    ChromeToolbarColor = Constants.UI.WebBrowserNavColor.ToShareColor(),
                    SafariControlTintColor = Constants.UI.WebBrowserNavColor.ToShareColor()
                });
        }

        void VisitSmartyPNet()
        {
            VisitWebsite(Constants.App.SmartyPNetUrl);
        }

        void VisitSPCWebsite()
        {
            VisitWebsite(Constants.App.SPCWebsiteUrl);
        }

        void VisitGithub()
        {
            VisitWebsite(Constants.App.GithubUrl);
        }

        async void Close()
        {
            await NavigationService.GoBackAsync(useModalNavigation: true);
        }
    }
}