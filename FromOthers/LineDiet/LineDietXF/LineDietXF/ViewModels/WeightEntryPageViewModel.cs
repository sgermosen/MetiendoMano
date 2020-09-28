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
    /// Weight entry screen shown modally to user when they go to add or edit a weight
    /// </summary>
    public class WeightEntryPageViewModel : BaseViewModel, INavigationAware
    {
        // Bindable Properties
        string _titleText = string.Empty;
        public string TitleText
        {
            get { return _titleText; }
            set { SetProperty(ref _titleText, value); }
        }

        DateTime _date = DateTime.Today;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                SetProperty(ref _date, value);
                UpdateWeightFieldFromDate(value);
                SaveCommand.RaiseCanExecuteChanged(); // anytime this value changes re-evaluate if the save button should be enabled
            }
        }

        string _weight = string.Empty;
        public string Weight
        {
            get { return _weight; }
            set
            {
                SetProperty(ref _weight, value);
                SaveCommand.RaiseCanExecuteChanged(); // anytime this value changes re-evaluate if the save button should be enabled
            }
        }

        string _weightStones = string.Empty;
        public string WeightStones
        {
            get { return _weightStones; }
            set
            {
                SetProperty(ref _weightStones, value);
                SaveCommand.RaiseCanExecuteChanged(); // anytime this value changes re-evaluate if the save button should be enabled
            }
        }

        string _weightStonePounds = string.Empty;
        public string WeightStonePounds
        {
            get { return _weightStonePounds; }
            set
            {
                SetProperty(ref _weightStonePounds, value);
                SaveCommand.RaiseCanExecuteChanged(); // anytime this value changes re-evaluate if the save button should be enabled
            }
        }

        bool _showPoundsEntryFields;
        public bool ShowStonesEntryFields
        {
            get { return _showPoundsEntryFields; }
            set { SetProperty(ref _showPoundsEntryFields, value); }
        }

        public string WeightLabel
        {
            get
            {
                return string.Format(Constants.Strings.WeightEntrylPage_WeightLabel, SettingsService.WeightUnit.ToSentenceUsageName());
            }
        }

        // Bindable Commands
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }

        // Services
        IDataService DataService { get; set; }

        // Private variables        
        bool _isUpdating = false;

        public WeightEntryPageViewModel(INavigationService navigationService, ISettingsService settingsService, IAnalyticsService analyticsService, IPageDialogService dialogService, IDataService dataService) :
            base(navigationService, settingsService, analyticsService, dialogService)
        {
            DataService = dataService;

            SaveCommand = new DelegateCommand(Save, SaveCanExecute); // SaveCanExecute dictates whether the command can fire, and in turn disables the button if not
            CloseCommand = new DelegateCommand(Close);
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            ShowStonesEntryFields = SettingsService.WeightUnit == WeightUnitEnum.StonesAndPounds;

            // see if we were passed in a weight entry to edit
            if (parameters != null && parameters.ContainsKey(nameof(WeightEntry)))
            {
                var weightEntry = parameters[nameof(WeightEntry)] as WeightEntry;

                _isUpdating = true;
                TitleText = Constants.Strings.WeightEntryPage_Title_Update;
                Date = weightEntry.Date;

                // show the previous weight for this date (different fields populated for stones units)
                if (ShowStonesEntryFields)
                {
                    var weightInStones = weightEntry.Weight.ToStonesAndPounds();
                    WeightStones = weightInStones.Stones.ToString();
                    WeightStonePounds = string.Format(Constants.Strings.Common_WeightFormat, weightInStones.Pounds);
                }
                else
                {
                    Weight = weightEntry.ToString();
                }                
            }
            else
            {
                _isUpdating = false;
                TitleText = Constants.Strings.WeightEntryPage_Title_Add;
                Date = DateTime.Today.Date;
                Weight = string.Empty;
                WeightStones = string.Empty;
                WeightStonePounds = string.Empty;
            }
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            AnalyticsService.TrackPageView(Constants.Analytics.Page_WeightEntry);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        StonesAndPounds GetWeightInStones()
        {
            if (string.IsNullOrWhiteSpace(WeightStones))
                return null;

            int weightStones;
            if (!int.TryParse(WeightStones, NumberStyles.Integer, CultureInfo.CurrentCulture, out weightStones))
                return null;

            // NOTE:: we will consider a blank pounds field as 0 pounds - the stones field is only required
            decimal weightPounds = 0;
            if (!string.IsNullOrEmpty(WeightStonePounds) && !decimal.TryParse(WeightStonePounds, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out weightPounds))
                return null;

            // don't allow negative values
            if (weightStones < 0 || weightPounds < 0)
                return null;

            // don't allow pounds to be 14 or more (would be another stone)
            if (weightPounds >= Constants.App.PoundsInAStone)
                return null;

            return new StonesAndPounds(weightStones, weightPounds);
        }

        bool SaveCanExecute()
        {
            // disable save button if they pick a date in the future
            if (Date.Date > DateTime.Today.Date)
                return false;

            // disable save button if we can't parse the weight text they entered
            decimal weightValue;

            if (ShowStonesEntryFields)
            {
                var weightInStones = GetWeightInStones();
                if (weightInStones == null)
                    return false;

                weightValue = weightInStones.ToPoundsDecimal();
            }
            else
            {
                if (!decimal.TryParse(Weight, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out weightValue))
                    return false;

                // disable save button if they enter a negative number or 0
                if (weightValue <= 0)
                    return false;
            }

            return true;
        }

        async void UpdateWeightFieldFromDate(DateTime date)
        {
            try
            {
                IncrementPendingRequestCount();

                var existingWeight = await DataService.GetWeightEntryForDate(date);
                if (existingWeight == null)
                    return;

                if (ShowStonesEntryFields)
                {
                    var stonesAndPounds = existingWeight.Weight.ToStonesAndPounds();
                    WeightStones = stonesAndPounds.Stones.ToString();
                    WeightStonePounds = string.Format(Constants.Strings.Common_WeightFormat, stonesAndPounds.Pounds);
                }
                else
                {
                    Weight = existingWeight.ToString();
                }
            }
            catch (Exception ex)
            {
                AnalyticsService.TrackFatalError($"{nameof(UpdateWeightFieldFromDate)} - an exception occurred.", ex);
                // NOTE:: not showing an error here as this is not in response to user action. potentially should show a non-intrusive error banner
            }
            finally
            {
                DecrementPendingRequestCount();
            }
        }

        async void Save()
        {
            AnalyticsService.TrackEvent(Constants.Analytics.WeightEntryCategory, _isUpdating ? Constants.Analytics.WeightEntry_UpdateWeight : Constants.Analytics.WeightEntry_AddedWeight, 1);

            // convert entered value to a valid weight
            bool parsedWeightValues = true;
            decimal weightValue = 0.0M;
            if (ShowStonesEntryFields)
            {
                var weightStoneFields = GetWeightInStones();
                if (weightStoneFields == null)
                {
                    parsedWeightValues = false;
                }
                else
                {
                    weightValue = weightStoneFields.ToPoundsDecimal();
                }
            }
            else
            {
                if (!decimal.TryParse(Weight, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out weightValue))
                {
                    parsedWeightValues = false;
                }
            }

            if (!parsedWeightValues)
            {
                // show error about invalid value if we can't convert the entered value to a decimal
                await DialogService.DisplayAlertAsync(Constants.Strings.WeightEntryPage_InvalidWeight_Title,
                    Constants.Strings.WeightEntryPage_InvalidWeight_Message,
                    Constants.Strings.Generic_OK);

                return;
            }

            try
            {
                IncrementPendingRequestCount();

                // see if they already have an entry for this date and if so get them to confirm it being replaced
                var existingEntry = await DataService.GetWeightEntryForDate(Date);
                if (existingEntry != null)
                {
                    // if we aren't updating, and the values aren't equal then show a warning (no need to show warning if the weights are the same)
                    if (!_isUpdating && existingEntry.Weight != weightValue)
                    {
                        // show different message for stones/pounds
                        string warningMessage;
                        if (ShowStonesEntryFields)
                        {
                            var existingWeightInStones = existingEntry.Weight.ToStonesAndPounds();
                            var newWeightInStones = weightValue.ToStonesAndPounds();
                            warningMessage = string.Format(Constants.Strings.Common_UpdateExistingWeight_Message,
                                string.Format(Constants.Strings.Common_Stones_WeightFormat, existingWeightInStones.Stones, existingWeightInStones.Pounds),
                                Date,
                                string.Format(Constants.Strings.Common_Stones_WeightFormat, newWeightInStones.Stones, newWeightInStones.Pounds));
                        }
                        else
                        {
                            warningMessage = string.Format(Constants.Strings.Common_UpdateExistingWeight_Message, existingEntry.Weight, Date, weightValue);
                        }
                        // show warning that an existing entry will be updated (is actually deleted and re-added), allow them to cancel
                        var result = await DialogService.DisplayAlertAsync(Constants.Strings.Common_UpdateExistingWeight_Title,
                            warningMessage,
                            Constants.Strings.Generic_OK,
                            Constants.Strings.Generic_Cancel);

                        // if they canceled the dialog then return without changing anything
                        if (!result)
                            return;
                    }

                    // remove existing weight now that they've confirmed
                    if (!await DataService.RemoveWeightEntryForDate(Date))
                    {
                        AnalyticsService.TrackError($"{nameof(Save)} - an error occurred trying to remove existing weight entry");

                        await DialogService.DisplayAlertAsync(Constants.Strings.Common_SaveError,
                            Constants.Strings.WeightEntryPage_Save_RemoveExistingWeightFailed_Message, Constants.Strings.Generic_OK);

                        return;
                    }
                }

                // create a new weight entry and add it
                var entry = new WeightEntry(Date, weightValue, SettingsService.WeightUnit);
                if (!await DataService.AddWeightEntry(entry))
                {
                    AnalyticsService.TrackError($"{nameof(Save)} - an error occurred trying to add new weight entry");

                    await DialogService.DisplayAlertAsync(Constants.Strings.Common_SaveError,
                        Constants.Strings.WeightEntryPage_Save_AddingWeightFailed_Message, Constants.Strings.Generic_OK);
                    return;
                }

                // see if the new weight entered matches the start date of the goal, if so update the goal with the new weight
                var goal = await DataService.GetGoal();
                if (goal != null && goal.StartDate.Date == Date)
                {
                    var success = true;
                    if (!await DataService.RemoveGoal())
                        success = false;

                    if (success)
                    { 
                        if (!await DataService.SetGoal(new WeightLossGoal(goal.StartDate, weightValue, goal.GoalDate, goal.GoalWeight, SettingsService.WeightUnit)))
                            success = false;
                    }

                    if (!success)
                    {
                        AnalyticsService.TrackError($"{nameof(Save)} - an error occurred trying to update start date of existing goal");

                        await DialogService.DisplayAlertAsync(Constants.Strings.Common_SaveError,
                            Constants.Strings.WeightEntryPage_Save_RemoveExistingWeightFailed_Message, Constants.Strings.Generic_OK);

                        return;
                    }
                }

                await NavigationService.GoBackAsync(useModalNavigation: true); // pop the dialog and return home
            }
            catch (Exception ex)
            {
                AnalyticsService.TrackFatalError($"{nameof(Save)} - an exception occurred.", ex);

                await DialogService.DisplayAlertAsync(Constants.Strings.Common_SaveError,
                    Constants.Strings.WeightEntryPage_Save_Exception_Message, Constants.Strings.Generic_OK);
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