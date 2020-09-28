using LineDietXF.Enumerations;
using LineDietXF.Interfaces;
using LineDietXF.Types;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Threading.Tasks;

namespace LineDietXF.ViewModels
{
    /// <summary>
    /// There is an extra debug menu item when the app is run in debug mode - this is the debug page that is shown. It contains
    /// a few buttons for testing the color changing of the app and for populating weight/goal test data.
    /// </summary>
    public class DebugPageViewModel : BaseViewModel
    {
        // Bindable Commands
        public DelegateCommand CloseCommand { get; set; }
        public DelegateCommand TurnGrayCommand { get; set; }
        public DelegateCommand TurnGreenCommand { get; set; }
        public DelegateCommand TurnRedCommand { get; set; }
        public DelegateCommand TestEndingAGoalCommand { get; set; }
        public DelegateCommand TestRealDataSetCommand { get; set; }
        public DelegateCommand TestLargeDataSetCommand { get; set; }

        // Services        
        IDataService DataService { get; set; }
        IEventAggregator EventAggregator { get; set; }
        IWindowColorService WindowColorService { get; set; }

        public DebugPageViewModel(INavigationService navigationService, ISettingsService settingsService, IAnalyticsService analyticsService, IPageDialogService dialogService, IDataService dataService, IEventAggregator eventAggregator, IWindowColorService windowColorService) :
            base(navigationService, settingsService, analyticsService, dialogService)
        {
            // Store off injected services
            DataService = dataService;
            EventAggregator = eventAggregator;
            WindowColorService = windowColorService;

            // Wire up commands
            CloseCommand = new DelegateCommand(Close);
            TurnGrayCommand = new DelegateCommand(TurnGray);
            TurnGreenCommand = new DelegateCommand(TurnGreen);
            TurnRedCommand = new DelegateCommand(TurnRed);
            TestEndingAGoalCommand = new DelegateCommand(TestEndingAGoal);
            TestRealDataSetCommand = new DelegateCommand(TestRealDataSet);
            TestLargeDataSetCommand = new DelegateCommand(TestLargeDataSet);
        }

        async Task ClearData()
        {
            try
            {
                IncrementPendingRequestCount();

                // remove goal
                if (!await DataService.RemoveGoal())
                {
                    await DialogService.DisplayAlertAsync("Error", "An error occurred removing the goal", Constants.Strings.Generic_OK);
                    return;
                }

                // remove all weight entries
                var allEntries = await DataService.GetAllWeightEntries();
                foreach (var entry in allEntries)
                {
                    await DataService.RemoveWeightEntryForDate(entry.Date);
                }
            }
            finally
            {
                DecrementPendingRequestCount();
            }
        }

        async Task<bool> ShowLoseDataWarning()
        {
            if (SettingsService.WeightUnit != WeightUnitEnum.ImperialPounds)
            {
                await DialogService.DisplayAlertAsync("Wrong units", "These methods are only setup for use when using pounds as the units. Change to pounds in Settings before using these methods for creating test data, then change units back.",
                    Constants.Strings.Generic_OK);

                return false;
            }

            return await DialogService.DisplayAlertAsync("Are you sure?", "This will delete all data and set it back up with sample data, are you sure?",
                Constants.Strings.Generic_OK, Constants.Strings.Generic_Cancel);
        }

        async void TestEndingAGoal()
        {
            if (!await ShowLoseDataWarning())
                return;

            try
            {
                IncrementPendingRequestCount();

                await ClearData();

                // set a goal where today is the goal end date
                var goalStartDate = DateTime.Today.Date - TimeSpan.FromDays(30);
                var goalEndDate = DateTime.Today.Date;
                var goal = new WeightLossGoal(goalStartDate, 240, goalEndDate, 210, WeightUnitEnum.ImperialPounds);
                await DataService.SetGoal(goal);

                // add some weights
                await DataService.AddWeightEntry(new WeightEntry(goalStartDate + TimeSpan.FromDays(5), 235, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(goalStartDate + TimeSpan.FromDays(10), 230, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(goalStartDate + TimeSpan.FromDays(15), 225, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(goalStartDate + TimeSpan.FromDays(20), 220, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(goalStartDate + TimeSpan.FromDays(25), 215, WeightUnitEnum.ImperialPounds));
            }
            finally
            {
                DecrementPendingRequestCount();
            }

            Close();
        }

        // NOTE:: primarily used for screenshots, is based on specific dates
        async void TestRealDataSet()
        {
            if (!await ShowLoseDataWarning())
                return;

            try
            {
                IncrementPendingRequestCount();

                await ClearData();

                // set goal
                var goal = new WeightLossGoal(new DateTime(2017, 1, 1), 237.4M, new DateTime(2017, 7, 1), 200, WeightUnitEnum.ImperialPounds);
                await DataService.SetGoal(goal);

                // add some weights
                await DataService.AddWeightEntry(new WeightEntry(new DateTime(2017, 1, 1), 237.4M, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(new DateTime(2017, 1, 17), 235.2M, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(new DateTime(2017, 2, 4), 233.0M, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(new DateTime(2017, 2, 22), 230.8M, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(new DateTime(2017, 2, 27), 229.4M, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(new DateTime(2017, 2, 28), 228.6M, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(new DateTime(2017, 3, 1), 228.2M, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(new DateTime(2017, 3, 2), 227.0M, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(new DateTime(2017, 3, 3), 226.4M, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(new DateTime(2017, 3, 4), 227.0M, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(new DateTime(2017, 3, 5), 227.2M, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(new DateTime(2017, 3, 6), 226.0M, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(new DateTime(2017, 3, 7), 225.4M, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(new DateTime(2017, 3, 8), 225.4M, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(new DateTime(2017, 3, 9), 225.2M, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(new DateTime(2017, 3, 10), 225.8M, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(new DateTime(2017, 3, 11), 225.2M, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(new DateTime(2017, 3, 12), 223.8M, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(new DateTime(2017, 3, 13), 223.5M, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(new DateTime(2017, 3, 14), 223.0M, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(new DateTime(2017, 3, 15), 221.8M, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(new DateTime(2017, 3, 16), 221.6M, WeightUnitEnum.ImperialPounds));
                await DataService.AddWeightEntry(new WeightEntry(new DateTime(2017, 3, 17), 221.4M, WeightUnitEnum.ImperialPounds));
            }
            finally
            {
                DecrementPendingRequestCount();
            }

            Close();
        }

        async void TestLargeDataSet()
        {
            if (!await ShowLoseDataWarning())
                return;

            try
            {
                IncrementPendingRequestCount();

                await ClearData();

                // set goal
                var goal = new WeightLossGoal(new DateTime(2017, 1, 1), 200, new DateTime(2017, 7, 1), 200, WeightUnitEnum.ImperialPounds);
                await DataService.SetGoal(goal);

                // add some weights
                DateTime date = new DateTime(2017, 3, 29);
                TimeSpan day = TimeSpan.FromDays(1);
                Random random = new Random();                
                for (int i = 0; i < 1000; i++)
                {
                    date = date - day;
                    var weightEntry = new WeightEntry(date, 200 + (decimal)(random.NextDouble() * 10) - 5, WeightUnitEnum.ImperialPounds);
                    await DataService.AddWeightEntry(weightEntry);
                }
            }
            finally
            {
                DecrementPendingRequestCount();
            }

            Close();
        }

        async void TurnGray()
        {
            if (!await ShowLoseDataWarning())
                return;

            try
            {
                IncrementPendingRequestCount();

                await ClearData(); // screen will be gray after clearing data
            }
            finally
            {
                DecrementPendingRequestCount();
            }

            Close();
        }

        async void TurnRed()
        {
            if (!await ShowLoseDataWarning())
                return;

            try
            {
                IncrementPendingRequestCount();

                await ClearData();

                // set a goal where today is the goal end date
                var goalStartDate = DateTime.Today.Date - TimeSpan.FromDays(30);
                var goalEndDate = DateTime.Today.Date;
                var goal = new WeightLossGoal(goalStartDate, 240, goalEndDate, 210, WeightUnitEnum.ImperialPounds);
                await DataService.SetGoal(goal);

                await DataService.AddWeightEntry(new WeightEntry(DateTime.Today.Date, 250, WeightUnitEnum.ImperialPounds));
            }
            finally
            {
                DecrementPendingRequestCount();
            }

            Close();
        }

        async void TurnGreen()
        {
            if (!await ShowLoseDataWarning())
                return;

            try
            {
                IncrementPendingRequestCount();

                await ClearData();

                // set a goal where today is the goal end date
                var goalStartDate = DateTime.Today.Date - TimeSpan.FromDays(30);
                var goalEndDate = DateTime.Today.Date;
                var goal = new WeightLossGoal(goalStartDate, 240, goalEndDate, 210, WeightUnitEnum.ImperialPounds);
                await DataService.SetGoal(goal);

                await DataService.AddWeightEntry(new WeightEntry(DateTime.Today.Date, 210, WeightUnitEnum.ImperialPounds));
            }
            finally
            {
                DecrementPendingRequestCount();
            }

            Close();
        }

        async void Close()
        {
            await NavigationService.GoBackAsync(useModalNavigation: true);
        }
    }
}