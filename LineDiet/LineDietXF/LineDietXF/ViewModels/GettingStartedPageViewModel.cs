using LineDietXF.Interfaces;
using LineDietXF.Types;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LineDietXF.ViewModels
{
    /// <summary>
    /// Getting started carousel shown to user on first run or through relative menu item
    /// </summary>
    public class GettingStartedPageViewModel : BaseViewModel, INavigationAware
    {
        // Bindable Properties
        ObservableCollection<GettingStartedCarouselItem> _carouselItems;
        public ObservableCollection<GettingStartedCarouselItem> CarouselItems
        {
            get { return _carouselItems; }
            set { SetProperty(ref _carouselItems, value); }
        }

        GettingStartedCarouselItem _selectedCarouselItem;
        public GettingStartedCarouselItem SelectedCarouselItem
        {
            get { return _selectedCarouselItem; }
            set { SetProperty(ref _selectedCarouselItem, value); }
        }

        // Bindable Commands
        public DelegateCommand CloseCommand { get; set; }

        public GettingStartedPageViewModel(INavigationService navigationService, ISettingsService settingsService, IAnalyticsService analyticsService, IPageDialogService dialogService) :
            base(navigationService, settingsService, analyticsService, dialogService)
        {
            CloseCommand = new DelegateCommand(Close);

            // setup carousel pages
            var carouselItems = new List<GettingStartedCarouselItem>();
            carouselItems.Add(new GettingStartedCarouselItem(Constants.Strings.GettingStarted_Page1Title, "GettingStartedIcon", Constants.Strings.GettingStarted_Page1Text));
            carouselItems.Add(new GettingStartedCarouselItem(Constants.Strings.GettingStarted_Page2Title, "GettingStartedSetGoal", Constants.Strings.GettingStarted_Page2Text));
            carouselItems.Add(new GettingStartedCarouselItem(Constants.Strings.GettingStarted_Page3Title, "GettingStartedAppColors", Constants.Strings.GettingStarted_Page3Text));
            carouselItems.Add(new GettingStartedCarouselItem(Constants.Strings.GettingStarted_Page4Title, "GettingStartedGraph", Constants.Strings.GettingStarted_Page4Text));
            CarouselItems = new ObservableCollection<GettingStartedCarouselItem>(carouselItems);
            SelectedCarouselItem = CarouselItems.First();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            parameters.Add(Constants.App.NavParam_FromGettingStarted, true);
        }

        public void OnNavigatingTo(NavigationParameters parameters) { }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            AnalyticsService.TrackPageView(Constants.Analytics.Page_GettingStarted);
        }

        async void Close()
        {
            await NavigationService.GoBackAsync(useModalNavigation: true);
        }
    }
}