using LineDietXF.Enumerations;
using LineDietXF.Extensions;
using LineDietXF.Helpers;
using LineDietXF.Interfaces;
using LineDietXF.Views;
using Plugin.Share;
using Plugin.Share.Abstractions;
using Prism;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace LineDietXF.ViewModels
{
    /// <summary>
    /// Third tab of the app - the main menu
    /// </summary>
    public class MenuPageViewModel : BaseViewModel, IActiveAware, INavigatedAware
    {
        List<MenuItem> _menuEntries;
        public List<MenuItem> MenuEntries
        {
            get { return _menuEntries; }
            set { SetProperty(ref _menuEntries, value); }
        }

        public MenuItem SelectedMenuItem
        {
            get { return null; }
            set
            {
                if (value != null)
                    NavigateToPage(value);
            }
        }

        #region IActiveAware implementation

        public event EventHandler IsActiveChanged;

        bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                if (IsActiveChanged != null)
                    IsActiveChanged(this, EventArgs.Empty);

                if (_isActive)
                    Setup();
                else
                    TearDown();
            }
        }

        #endregion

        // Services
        IDataService DataService { get; set; }
        IWindowColorService WindowColorService { get; set; }
        IReviewService ReviewService { get; set; }

        public MenuPageViewModel(INavigationService navigationService, ISettingsService settingsService, IAnalyticsService analyticsService, IPageDialogService dialogService, IDataService dataService, IWindowColorService windowColorService, IReviewService reviewService) :
            base(navigationService, settingsService, analyticsService, dialogService)
        {
            // Store off services
            DataService = dataService;
            WindowColorService = windowColorService;
            ReviewService = reviewService;

            // Build up menu
            MenuEntries = BuildMenu();
        }

        public void OnNavigatedFrom(NavigationParameters parameters) { }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            AnalyticsService.TrackPageView(Constants.Analytics.Page_Menu);

            if (parameters != null && parameters.ContainsKey(Constants.App.NavParam_FromGettingStarted) && (bool)parameters[Constants.App.NavParam_FromGettingStarted] == true)
            {
                await DialogService.DisplayAlertAsync(Constants.Strings.Common_SettingWeightUnits_Title,
                    string.Format(Constants.Strings.Common_SettingWeightUnits_Message, SettingsService.WeightUnit.ToSentenceUsageName()),
                    Constants.Strings.Generic_OK);
            }
        }

        public void Setup()
        {
            // wire up events
            DataService.UserDataUpdated += DataService_UserDataUpdated;

            // The first run of the app the data service may not have finished loading yet. In that scenario it will fire UserDataUpdated() once done init'ing which will cause the RefreshFromuserDataAsync() to be called.
            // On subsequent navigations it will have already been init'd and should refresh data when returning to this page
            if (DataService.HasBeenInitialized)
                RefreshFromUserDataAsync();
        }

        public void TearDown()
        {
            // unwire from events
            if (DataService != null)
                DataService.UserDataUpdated -= DataService_UserDataUpdated;
        }

        void DataService_UserDataUpdated(object sender, EventArgs e)
        {
            RefreshFromUserDataAsync();
        }

        async void RefreshFromUserDataAsync()
        {
            try
            {
                IncrementPendingRequestCount(); // show loading

                // do async data read                                
                var todaysWeightEntry = await DataService.GetWeightEntryForDate(DateTime.Today.Date);
                var goal = await DataService.GetGoal();

                // update the UI color (NOTE:: both goal and todaysWeightEntry could be null)
                var infoForToday = WeightLogicHelpers.GetTodaysDisplayInfo(goal, todaysWeightEntry);
                WindowColorService.ChangeAppBaseColor(infoForToday.ColorToShow);
            }
            catch (Exception ex)
            {
                AnalyticsService.TrackFatalError($"{nameof(RefreshFromUserDataAsync)} - an exception occurred.", ex);
                // NOTE:: not showing an error here as this is not in response to user action. potentially should show a non-intrusive error banner
            }
            finally
            {
                DecrementPendingRequestCount(); // hide loading
            }
        }

        List<MenuItem> BuildMenu()
        {
            var menuEntries = new List<MenuItem>();

            // add blank to top to add top space
            menuEntries.Add(new MenuItem(MenuItemEnum.Divider, string.Empty, true));

            menuEntries.Add(new MenuItem(MenuItemEnum.GettingStarted, Constants.Strings.Menu_GettingStarted, false));
            menuEntries.Add(new MenuItem(MenuItemEnum.SetGoal, Constants.Strings.Menu_SetGoal, false));
            menuEntries.Add(new MenuItem(MenuItemEnum.Settings, Constants.Strings.Menu_Settings, false));
            menuEntries.Add(new MenuItem(MenuItemEnum.Divider, string.Empty, true));

            menuEntries.Add(new MenuItem(MenuItemEnum.Share, Constants.Strings.Menu_Share, false));
            menuEntries.Add(new MenuItem(MenuItemEnum.LeaveAReview, Constants.Strings.Menu_LeaveAReview, false));
            menuEntries.Add(new MenuItem(MenuItemEnum.SendFeedback, Constants.Strings.Menu_SendFeedback, false));
            menuEntries.Add(new MenuItem(MenuItemEnum.About, Constants.Strings.Menu_About, false));

#if DEBUG
            // debug menu only shown when running in debug mode
            menuEntries.Add(new MenuItem(MenuItemEnum.Divider, string.Empty, true));
            menuEntries.Add(new MenuItem(MenuItemEnum.Debug, Constants.Strings.Menu_Debug, false));
#endif

            // add blank to top to add bottom space
            menuEntries.Add(new MenuItem(MenuItemEnum.Divider, string.Empty, true));

            return menuEntries;
        }

        public async void NavigateToPage(MenuItem menuEntry)
        {
            switch (menuEntry.MenuType)
            {
                case MenuItemEnum.GettingStarted:
                    AnalyticsService.TrackEvent(Constants.Analytics.MenuCategory, Constants.Analytics.Menu_GetingStarted, 1);
                    await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(GettingStartedPage)}", useModalNavigation: true);
                    break;
                case MenuItemEnum.SetGoal:
                    AnalyticsService.TrackEvent(Constants.Analytics.MenuCategory, Constants.Analytics.Menu_SetGoal, 1);
                    await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(SetGoalPage)}", useModalNavigation: true);
                    break;
                case MenuItemEnum.Settings:
                    AnalyticsService.TrackEvent(Constants.Analytics.MenuCategory, Constants.Analytics.Menu_Settings, 1);
                    await NavigationService.NavigateAsync($"{nameof(SettingsPage)}", useModalNavigation: false);
                    break;
                case MenuItemEnum.Share:
                    AnalyticsService.TrackEvent(Constants.Analytics.MenuCategory, Constants.Analytics.Menu_Share, 1);
                    ShareAppLink();
                    break;
                case MenuItemEnum.LeaveAReview:
                    AnalyticsService.TrackEvent(Constants.Analytics.MenuCategory, Constants.Analytics.Menu_LeaveAReview, 1);
                    LeaveAReview();
                    break;
                case MenuItemEnum.SendFeedback:
                    AnalyticsService.TrackEvent(Constants.Analytics.MenuCategory, Constants.Analytics.Menu_SendFeedback, 1);
                    SendFeedback();
                    break;
                case MenuItemEnum.About:
                    AnalyticsService.TrackEvent(Constants.Analytics.MenuCategory, Constants.Analytics.Menu_About, 1);
                    await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(AboutPage)}", useModalNavigation: true);
                    break;
                case MenuItemEnum.Debug:
                    AnalyticsService.TrackEvent(Constants.Analytics.MenuCategory, Constants.Analytics.Menu_Debug, 1);
#if DEBUG
                    await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(DebugPage)}", useModalNavigation: true);
#else
                    AnalyticsService.TrackFatalError($"{nameof(NavigateToPage)} - should not be able to use Debug menu in release mode - doing nothing");
#endif
                    break;
            }
        }

        void LeaveAReview()
        {
            var reviewSuccess = ReviewService.LeaveAReview();
            if (!reviewSuccess)
            {
                var errorMessage = string.Empty;
                if (Device.OS == TargetPlatform.Android)
                    errorMessage = string.Format(Constants.Strings.Review_Failed_Message, Constants.Strings.Review_Failed_Part_Android);
                else if (Device.OS == TargetPlatform.iOS)
                    errorMessage = string.Format(Constants.Strings.Review_Failed_Message, Constants.Strings.Review_Failed_Part_iOS);
                else
                {
                    // TODO:: NEWPLATFORMS:: Implement platform specific error message
                    errorMessage = string.Format(Constants.Strings.Review_Failed_Message, Constants.Strings.Review_Failed_Part_Generic);
                }

                DialogService.DisplayAlertAsync(Constants.Strings.Review_Failed_Title, errorMessage, Constants.Strings.Generic_OK);
            }
        }

        async void SendFeedback()
        {
            await CrossShare.Current.OpenBrowser(Constants.App.FeedbackUrl,
                new BrowserOptions()
                {
                    SafariBarTintColor = Constants.UI.WebBrowserNavColor.ToShareColor(),
                    ChromeToolbarColor = Constants.UI.WebBrowserNavColor.ToShareColor(),
                    SafariControlTintColor = Constants.UI.WebBrowserNavColor.ToShareColor()
                });
        }

        void ShareAppLink()
        {
            CrossShare.Current.Share(new ShareMessage()
            {
                Title = Constants.Strings.ShareTitle,
                Text = Constants.Strings.ShareMessage,
                Url = Constants.App.ShareUrl
            });
        }
    }
}