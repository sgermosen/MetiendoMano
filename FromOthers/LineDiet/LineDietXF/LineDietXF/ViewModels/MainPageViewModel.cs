using LineDietXF.Interfaces;
using Prism.Navigation;
using Prism.Services;

namespace LineDietXF.ViewModels
{
    /// <summary>
    /// The Main Page of the app which contains the main tabs of the app. Is effectively just a wrapper to the tabs.
    /// </summary>
    public partial class MainPageViewModel : BaseViewModel, INavigatedAware
    {
        public MainPageViewModel(INavigationService navigationService, ISettingsService settingsService, IAnalyticsService analyticsService,
                                 IPageDialogService dialogService) :
            base(navigationService, settingsService, analyticsService, dialogService)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            AnalyticsService.TrackPageView(Constants.Analytics.Page_Main);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }
    }
}