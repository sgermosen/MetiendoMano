using LineDietXF.Enumerations;
using LineDietXF.Extensions;
using LineDietXF.Helpers;
using LineDietXF.Interfaces;
using LineDietXF.Types;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Globalization;

namespace LineDietXF.ViewModels
{
    /// <summary>
    /// Set a goal page shown modally from first tab of app or main menu
    /// </summary>
    public class SetGoalPageViewModel : BaseViewModel, INavigationAware
    {
        // Bindable Properties
        DateTime _startDate = DateTime.Today.Date;
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                SetProperty(ref _startDate, value);
                UpdateStartWeightFromStartDate();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        string _startWeight;
        public string StartWeight
        {
            get { return _startWeight; }
            set
            {
                SetProperty(ref _startWeight, value);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        string _startWeightStones;
        public string StartWeightStones
        {
            get { return _startWeightStones; }
            set
            {
                SetProperty(ref _startWeightStones, value);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        string _startWeightStonePounds;
        public string StartWeightStonePounds
        {
            get { return _startWeightStonePounds; }
            set
            {
                SetProperty(ref _startWeightStonePounds, value);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        string _goalWeightStones;
        public string GoalWeightStones
        {
            get { return _goalWeightStones; }
            set
            {
                SetProperty(ref _goalWeightStones, value);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        string _goalWeightStonePounds;
        public string GoalWeightStonePounds
        {
            get { return _goalWeightStonePounds; }
            set
            {
                SetProperty(ref _goalWeightStonePounds, value);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        DateTime _goalDate = DateTime.Today.AddMonths(Constants.App.DefaultGoalDateOffsetInMonths);
        public DateTime GoalDate
        {
            get { return _goalDate; }
            set
            {
                SetProperty(ref _goalDate, value);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        string _goalWeight;
        public string GoalWeight
        {
            get { return _goalWeight; }
            set
            {
                SetProperty(ref _goalWeight, value);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        bool _showPoundsEntryFields;
        public bool ShowStonesEntryFields
        {
            get { return _showPoundsEntryFields; }
            set { SetProperty(ref _showPoundsEntryFields, value); }
        }

        public string StartWeightLabel
        {
            get
            {
                return string.Format(Constants.Strings.SetGoalPage_StartWeightLabel, SettingsService.WeightUnit.ToSentenceUsageName());
            }
        }

        public string GoalWeightLabel
        {
            get
            {
                return string.Format(Constants.Strings.SetGoalPage_GoalWeightLabel, SettingsService.WeightUnit.ToSentenceUsageName());
            }
        }

        // Services
        IDataService DataService { get; set; }

        // Commands
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }

        public SetGoalPageViewModel(INavigationService navigationService, ISettingsService settingsService, IAnalyticsService analyticsService, IPageDialogService dialogService, IDataService dataService) :
            base(navigationService, settingsService, analyticsService, dialogService)
        {
            // Store off injected services
            DataService = dataService;

            // Setup bindable commands
            SaveCommand = new DelegateCommand(Save, SaveCanExecute);
            CloseCommand = new DelegateCommand(Close);
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            ShowStonesEntryFields = SettingsService.WeightUnit == WeightUnitEnum.StonesAndPounds;
            UpdateStartWeightFromStartDate();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            AnalyticsService.TrackPageView(Constants.Analytics.Page_SetGoal);

            TryLoadExistingGoal();
        }

        public void OnNavigatedFrom(NavigationParameters parameters) { }

        async void TryLoadExistingGoal()
        {
            WeightLossGoal existingGoal;
            try
            {
                IncrementPendingRequestCount(); // show loading

                existingGoal = await DataService.GetGoal();
            }
            catch (Exception ex)
            {
                AnalyticsService.TrackFatalError($"{nameof(TryLoadExistingGoal)} - an exception occurred.", ex);
                // NOTE:: not showing an error here as this is not in response to user action. potentially should show a non-intrusive error banner
                return;
            }
            finally
            {
                DecrementPendingRequestCount(); // hide loading
            }

            if (existingGoal == null)
                return;

            // NOTE:: we could see if the goal date was already past and not load it in that case, but it would be better to still
            // bring in what they had before and just let them update it (ex: moving goal date forward / back)
            StartDate = existingGoal.StartDate;            
            GoalDate = existingGoal.GoalDate;

            // setup entry fields for start and goal weights (we have different fields for stones (2 fields) than kg/pounds)
            if (ShowStonesEntryFields)
            {
                var startWeightStones = existingGoal.StartWeight.ToStonesAndPounds();
                var goalWeightStones = existingGoal.GoalWeight.ToStonesAndPounds();

                StartWeightStones = startWeightStones.Stones.ToString();
                StartWeightStonePounds = string.Format(Constants.Strings.Common_WeightFormat, startWeightStones.Pounds);

                GoalWeightStones = goalWeightStones.Stones.ToString();
                GoalWeightStonePounds = string.Format(Constants.Strings.Common_WeightFormat, goalWeightStones.Pounds);
            }
            else
            {
                StartWeight = string.Format(Constants.Strings.Common_WeightFormat, existingGoal.StartWeight);
                GoalWeight = string.Format(Constants.Strings.Common_WeightFormat, existingGoal.GoalWeight);
            }
        }

        async void UpdateStartWeightFromStartDate()
        {
            // pre-populate today's weight field if it has a value
            try
            {
                IncrementPendingRequestCount();

                var existingStartDateWeight = await DataService.GetWeightEntryForDate(StartDate);
                if (existingStartDateWeight != null)
                {
                    if (ShowStonesEntryFields)
                    {
                        var startWeightStones = existingStartDateWeight.Weight.ToStonesAndPounds();
                        StartWeightStones = startWeightStones.Stones.ToString();
                        StartWeightStonePounds = string.Format(Constants.Strings.Common_WeightFormat, startWeightStones.Pounds);
                    }
                    else
                    {
                        StartWeight = existingStartDateWeight.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                AnalyticsService.TrackFatalError($"{nameof(UpdateStartWeightFromStartDate)} - an exception occurred.", ex);
                // NOTE:: not showing an error here as this is not in response to user action. potentially should show a non-intrusive error banner
            }
            finally
            {
                DecrementPendingRequestCount();
            }
        }

        bool SaveCanExecute()
        {
            // disable the save button if their goal date is before their start date or the dates are equal
            if (GoalDate <= StartDate)
                return false;

            if (ShowStonesEntryFields) // logic for pounds entry (two fields)
            {
                // disable the save button if weight fields can't be parsed
                if (GetStartWeightInStones() == null || GetGoalWeightInStones() == null)
                    return false;
            }
            else // logic for single field (kg or pounds)
            { 
                // disable the save button if either weight field is empty
                if (string.IsNullOrWhiteSpace(StartWeight) || string.IsNullOrWhiteSpace(GoalWeight))
                    return false;

                // disable the save button if either weight text field can't be parsed
                decimal startWeight, goalWeight;                
                if (!decimal.TryParse(StartWeight, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out startWeight) ||
                    !decimal.TryParse(GoalWeight, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out goalWeight))
                    return false;
            }

            return true;
        }

        StonesAndPounds GetStartWeightInStones()
        {
            if (string.IsNullOrWhiteSpace(StartWeightStones))
                return null;

            int startWeightStones;
            if (!int.TryParse(StartWeightStones, NumberStyles.Integer, CultureInfo.CurrentCulture, out startWeightStones))
                return null;

            // NOTE:: we will consider a blank pounds field as 0 pounds - the stones field is only required
            decimal startWeightPounds = 0;
            if (!string.IsNullOrEmpty(StartWeightStonePounds) && !decimal.TryParse(StartWeightStonePounds, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out startWeightPounds))
                return null;

            // don't allow negative values
            if (startWeightStones < 0 || startWeightPounds < 0)
                return null;

            // don't allow pounds to be 14 or more (would be another stone)
            if (startWeightPounds >= Constants.App.PoundsInAStone)
                return null;

            return new StonesAndPounds(startWeightStones, startWeightPounds);
        }

        StonesAndPounds GetGoalWeightInStones()
        {            
            if (string.IsNullOrWhiteSpace(GoalWeightStones))
                return null;

            int goalWeightStones;
            if (!int.TryParse(GoalWeightStones, NumberStyles.Integer, CultureInfo.CurrentCulture, out goalWeightStones))
                return null;

            // NOTE:: we will consider a blank pounds field as 0 pounds - the stones field is only required
            decimal goalWeightPounds = 0;
            if (!string.IsNullOrEmpty(GoalWeightStonePounds) && !decimal.TryParse(GoalWeightStonePounds, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out goalWeightPounds))
                return null;

            // don't allow negative values
            if (goalWeightStones < 0 || goalWeightPounds < 0)
                return null;

            // don't allow pounds to be 14 or more (would be another stone)
            if (goalWeightPounds >= Constants.App.PoundsInAStone)
                return null;

            return new StonesAndPounds(goalWeightStones, goalWeightPounds);
        }

        async void Save()
        {
            AnalyticsService.TrackEvent(Constants.Analytics.SetGoalCategory, Constants.Analytics.SetGoal_SavedGoal, 1);

            // convert entered value to a valid weight
            bool parsedWeightFields = true;
            decimal startWeight = 0;
            decimal goalWeight = 0;

            if (ShowStonesEntryFields)
            {
                var startWeightStoneFields = GetStartWeightInStones();
                var goalWeightStoneFields = GetGoalWeightInStones();

                if (startWeightStoneFields == null || goalWeightStoneFields == null)
                {
                    parsedWeightFields = false;
                }
                else
                {
                    startWeight = startWeightStoneFields.ToPoundsDecimal();
                    goalWeight = goalWeightStoneFields.ToPoundsDecimal();
                }
            }
            else
            {
                if (!decimal.TryParse(StartWeight, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out startWeight) || 
                    !decimal.TryParse(GoalWeight, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out goalWeight))
                    parsedWeightFields = false;                
            }

            if (!parsedWeightFields)
            {
                // show error about invalid value if we can't convert the entered value to a decimal
                await DialogService.DisplayAlertAsync(Constants.Strings.SetGoalPage_InvalidWeight_Title,
                    Constants.Strings.SetGoalPage_InvalidWeight_Message,
                    Constants.Strings.Generic_OK);

                return;
            }

            // give warning if goal weight is greater than start weight
            // NOTE:: we don't prevent this scenario as I have had friends intentionally use the previous version of line diet for
            // tracking weight gain during pregnancy or muscle building - so we just give a warning. We also don't prevent equal
            // start and goal weights in case they just want a line to show a maintenance weight they are trying to stay at
            if (goalWeight > startWeight)
            {
                // TODO:: analytics
                var result = await DialogService.DisplayAlertAsync(Constants.Strings.SetGoalPage_GoalWeightGreaterThanStartWeight_Title,
                    Constants.Strings.SetGoalpage_GoalWeightGreaterThanStartWeight_Message,
                    Constants.Strings.Generic_OK, Constants.Strings.Generic_Cancel);

                if (!result)
                    return;
            }

            try
            {
                IncrementPendingRequestCount();

                // see if they've entered a different weight already for this date, if so warn them about it being updated
                var existingEntry = await DataService.GetWeightEntryForDate(StartDate);
                if (existingEntry != null)
                {
                    if (existingEntry.Weight != startWeight)
                    {
                        // show different message for stones/pounds
                        string warningMessage;
                        if (ShowStonesEntryFields)
                        {
                            var existingWeightInStones = existingEntry.Weight.ToStonesAndPounds();
                            var startWeightInStones = startWeight.ToStonesAndPounds();

                            warningMessage = string.Format(Constants.Strings.Common_UpdateExistingWeight_Message, 
                                string.Format(Constants.Strings.Common_Stones_WeightFormat, existingWeightInStones.Stones, existingWeightInStones.Pounds),
                                StartDate, 
                                string.Format(Constants.Strings.Common_Stones_WeightFormat, startWeightInStones.Stones, startWeightInStones.Pounds));
                        }
                        else
                        {
                            warningMessage = string.Format(Constants.Strings.Common_UpdateExistingWeight_Message, existingEntry.Weight, StartDate, startWeight);
                        }

                        // show warning that an existing entry will be updated (is actually deleted and re-added), allow them to cancel
                        var result = await DialogService.DisplayAlertAsync(Constants.Strings.Common_UpdateExistingWeight_Title, warningMessage,
                            Constants.Strings.Generic_OK,
                            Constants.Strings.Generic_Cancel);

                        // if they canceled the dialog then return without changing anything
                        if (!result)
                            return;
                    }

                    // remove existing weight
                    if (!await DataService.RemoveWeightEntryForDate(StartDate))
                    {
                        AnalyticsService.TrackError($"{nameof(Save)} - Error when trying to remove existing weight entry for start date");

                        await DialogService.DisplayAlertAsync(Constants.Strings.Common_SaveError,
                            Constants.Strings.SetGoalPage_Save_RemoveExistingWeightFailed_Message, Constants.Strings.Generic_OK);
                        return;
                    }
                }

                var addStartWeightResult = await DataService.AddWeightEntry(new WeightEntry(StartDate, startWeight, SettingsService.WeightUnit));
                if (!addStartWeightResult)
                {
                    AnalyticsService.TrackError($"{nameof(Save)} - Error when trying to add weight entry for start date");

                    await DialogService.DisplayAlertAsync(Constants.Strings.Common_SaveError,
                        Constants.Strings.SetGoalPage_Save_AddingWeightFailed_Message, Constants.Strings.Generic_OK);
                    return;
                }

                var weightLossGoal = new WeightLossGoal(StartDate, startWeight, GoalDate.Date, goalWeight, SettingsService.WeightUnit);
                if (!await DataService.SetGoal(weightLossGoal))
                {
                    AnalyticsService.TrackError($"{nameof(Save)} - Error when trying to save new weight loss goal");

                    await DialogService.DisplayAlertAsync(Constants.Strings.Common_SaveError,
                        Constants.Strings.SetGoalPage_Save_AddingGoalFailed_Message, Constants.Strings.Generic_OK);
                    return;
                }

                await NavigationService.GoBackAsync(useModalNavigation: true);
            }
            catch (Exception ex)
            {
                AnalyticsService.TrackFatalError($"{nameof(Save)} - an exception occurred.", ex);

                await DialogService.DisplayAlertAsync(Constants.Strings.Common_SaveError,
                    Constants.Strings.SetGoalPage_Save_Exception_Message, Constants.Strings.Generic_OK);
            }
            finally
            {
                DecrementPendingRequestCount();
            }
        }

        void Close()
        {
            NavigationService.GoBackAsync(useModalNavigation: true);
        }
    }
}
