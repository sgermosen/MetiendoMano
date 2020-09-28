using LineDietXF.Enumerations;
using LineDietXF.Extensions;
using LineDietXF.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LineDietXF.Helpers
{
    /// <summary>
    /// Helper class for various weight calculations and messaging (ex: given a goal and today's weight, are they on track, etc.)
    /// </summary>
    public static class WeightLogicHelpers
    {
        public static string ToSettingsName(this WeightUnitEnum weightUnit)
        {
            switch (weightUnit)
            {
                case WeightUnitEnum.ImperialPounds:
                    return Constants.Strings.Settings_ImperialPound;
                case WeightUnitEnum.Kilograms:
                    return Constants.Strings.Settings_Kilograms;
                case WeightUnitEnum.StonesAndPounds:
                    return Constants.Strings.Settings_StonesAndPounds;
                default:
#if DEBUG
                    Debugger.Break();
#endif
                    return "??";
            }
        }

        public static string ToSentenceUsageName(this WeightUnitEnum weightUnit)
        {
            switch (weightUnit)
            {
                case WeightUnitEnum.ImperialPounds:
                    return Constants.Strings.Common_WeightUnit_ImperialPounds;
                case WeightUnitEnum.Kilograms:
                    return Constants.Strings.Common_WeightUnit_Kilograms;
                case WeightUnitEnum.StonesAndPounds:
                    // NOTE:: this method is not used for the summary info as stones are displayed as "X stones, X.X pounds", unlike other unit types
                    return Constants.Strings.Common_WeightUnit_Stones; 
                default:
#if DEBUG
                    Debugger.Break();
#endif
                    return "??";
            }
        }

        public static bool WeightMetGoalOnDate(WeightLossGoal goal, DateTime dt, decimal weightOnDate)
        {
            if (goal == null)
                return false;

            if (weightOnDate == decimal.MinValue)
                return false;

            var goalWeightForDate = GetGoalWeightForDate(goal, dt.Date);
            return weightOnDate <= goalWeightForDate;
        }

        public static Tuple<decimal, decimal> GetMinMaxWeightRange(WeightUnitEnum weightUnit, WeightLossGoal goal, List<WeightEntry> entries, DateTime dateStart, DateTime dateEnd)
        {
            if (entries.Count == 0)
            {
                if (goal != null)
                {
                    var goalPadding = (weightUnit == WeightUnitEnum.ImperialPounds || weightUnit == WeightUnitEnum.StonesAndPounds ? 
                        Constants.App.Graph_GoalOnly_Pounds_Padding : Constants.App.Graph_GoalOnly_Kilograms_Padding);
                    // there should normally be at least one weight if a goal is set, there is nothing really to graph but the goal line itself
                    return new Tuple<decimal, decimal>(goal.GoalWeight - goalPadding, goal.GoalWeight + goalPadding);
                }

                // no goal set and no saved weights, so just graph a default range
                var rangeDefaultMin = (weightUnit == WeightUnitEnum.ImperialPounds || weightUnit == WeightUnitEnum.StonesAndPounds ? 
                    Constants.App.Graph_WeightRange_Pounds_DefaultMin : Constants.App.Graph_WeightRange_Kilograms_DefaultMin);
                var rangeDefaultMax = (weightUnit == WeightUnitEnum.ImperialPounds || weightUnit == WeightUnitEnum.StonesAndPounds ? 
                    Constants.App.Graph_WeightRange_Pounds_DefaultMax : Constants.App.Graph_WeightRange_Kilograms_DefaultMax);
                return new Tuple<decimal, decimal>(rangeDefaultMin, rangeDefaultMax);
            }

            decimal minValue = decimal.MaxValue;
            decimal maxValue = decimal.MinValue;

            // if there is a goal set then we want to include that line for the date range we're looking at
            if (goal != null)
            {
                // now see if our goal is greater/less for start end dates, if so include those in the range
                decimal goalStart = GetGoalWeightForDate(goal, dateStart);
                if (goalStart != decimal.MinValue)
                {
                    if (goalStart < minValue)
                        minValue = goalStart;
                    if (goalStart > maxValue)
                        maxValue = goalStart;
                }

                decimal goalEnd = GetGoalWeightForDate(goal, dateEnd);
                if (goalEnd != decimal.MinValue)
                {
                    if (goalEnd < minValue)
                        minValue = goalEnd;
                    if (goalEnd > maxValue)
                        maxValue = goalEnd;
                }
            }

            foreach (var entry in entries)
            {
                if (entry.Weight < minValue)
                    minValue = (decimal)entry.Weight;
                if (entry.Weight > maxValue)
                    maxValue = (decimal)entry.Weight;
            }

            minValue = minValue * Constants.App.Graph_WeightRange_MinPadding;
            maxValue = maxValue * Constants.App.Graph_WeightRange_MaxPadding;

            minValue = (decimal)Math.Floor(minValue); // use whole numbers
            maxValue = (decimal)Math.Ceiling(maxValue);

            return new Tuple<decimal, decimal>(minValue, maxValue);
        }

        public static Tuple<DateTime, DateTime> GetGraphDateRange(List<WeightEntry> entries)
        {
            if (entries == null)
            {
                Debug.WriteLine($"{nameof(GetGraphDateRange)} - Passed in null WeightEntry collection!");
                if (Debugger.IsAttached)
                    Debugger.Break();

                return null;
            }

            // the graph falls apart over 120 days, so we force the window to a 30 day range around today's date
            DateTime dateRangeStart = DateTime.Today.AddDays(-15);
            DateTime dateRangeEnd = DateTime.Today.AddDays(15);

            // if they only have one entry, then lean the range forward a bit
            if (entries.Count == 1)
            {
                dateRangeStart = DateTime.Today.AddDays(-5);
                dateRangeEnd = DateTime.Today.AddDays(25);
            }

            return new Tuple<DateTime, DateTime>(dateRangeStart, dateRangeEnd);
        }

        public static InfoForToday GetTodaysDisplayInfo(WeightLossGoal goal, WeightEntry todaysWeightEntry)
        {
            var infoForToday = new InfoForToday();
            infoForToday.TodaysDisplayWeight = (todaysWeightEntry != null) ? todaysWeightEntry.ToString() : Constants.Strings.DailyInfoPage_UnknownWeight;

            if (goal == null)
            {
                infoForToday.IsEnterWeightButtonVisible = false;
                infoForToday.IsSetGoalButtonVisible = true;
            }
            else if (todaysWeightEntry == null)
            {
                infoForToday.IsEnterWeightButtonVisible = true;
                infoForToday.IsSetGoalButtonVisible = false;
            }

            infoForToday.TodaysMessage = GetTodaysMessageFromWeight(todaysWeightEntry, goal); // ok that either may be null

            // if they haven't set a goal, or don't have a weight today, or today is the first day of the diet, then we don't
            // want to colorize the screen or show the HowToEat information below their weight
            if (goal != null && todaysWeightEntry != null && goal.StartDate.Date != DateTime.Today.Date)
            {
                // update the window color if we have a weight for today and know their goal
                decimal goalWeightForToday = GetGoalWeightForDate(goal, DateTime.Today.Date);
                bool onADiet = todaysWeightEntry.Weight > goalWeightForToday;
                infoForToday.ColorToShow = onADiet ? BaseColorEnum.Red : BaseColorEnum.Green;
                infoForToday.HowToEatMessage = onADiet ? Constants.Strings.DailyInfoPage_HowToEat_OnDiet : Constants.Strings.DailyInfoPage_HowToEat_OffDiet;
            }

            return infoForToday;
        }

        static decimal GetGoalWeightForDate(WeightLossGoal goal, DateTime date)
        {
            // make sure we have a goal set first
            if (goal == null)
                throw new InvalidOperationException($"{nameof(GetGoalWeightForDate)} was passed in a null goal.");

            // if the date requested is before the goal start, then return the goal start weight
            if (date.Date <= goal.StartDate.Date)
                return goal.StartWeight;

            // if the date requested is after the goal end, then return the goal end weight
            if (date.Date >= goal.GoalDate.Date)
                return goal.GoalWeight;

            // NOTE:: calculation is: ((y2 - y1) / diet length in days) * days elapsed + y1
            // NOTE:: calculation is: start weight - ((start weight - end weight) / diet length in days) * days elapsed

            decimal weightDiff = goal.StartWeight - goal.GoalWeight;
            TimeSpan duration = goal.GoalDate.Date - goal.StartDate.Date;
            TimeSpan elapsed = date - goal.StartDate.Date;

            var goalWeightForDate = goal.StartWeight - ((weightDiff / (decimal)duration.TotalDays) * (decimal)elapsed.TotalDays);
            return goalWeightForDate;
        }

        // NOTE:: either of the parameters could be null if they are not yet entered
        static string GetTodaysMessageFromWeight(WeightEntry todaysEntry, WeightLossGoal goal)
        {
            // if they haven't set a goal
            if (goal == null)
                return Constants.Strings.DailyInfoPage_TodaysMessage_NoGoal;

            // if they started the goal today
            if (goal.StartDate.Date == DateTime.Today.Date)
                return Constants.Strings.DailyInfoPage_TodaysMessage_NewGoal;

            // if they haven't entered today's weight yet
            if (todaysEntry == null)
                return Constants.Strings.DailyInfoPage_TodaysMessage_NoWeightToday;

            decimal todaysGoalWeight = GetGoalWeightForDate(goal, DateTime.Today.Date);
            int daysToGo = (int)(Math.Floor((goal.GoalDate.Date - DateTime.Today.Date).TotalDays));

            // they have reached their goal weight (could be before or after the goal date, we show same message either way)
            if (todaysEntry.Weight <= goal.GoalWeight)
                return Constants.Strings.DailyInfoPage_GoalEnd_Success;

            // they've reached the goal date, show success/failure
            if (daysToGo <= 0)
            {
                return (todaysEntry.Weight <= todaysGoalWeight) ?
                    Constants.Strings.DailyInfoPage_GoalEnd_Success :
                    Constants.Strings.DailyInfoPage_GoalEnd_Failure;
            }

            // they are still working towards a goal, show a summary of their progress
            var amountLost = goal.StartWeight - todaysEntry.Weight;
            var amountRemaining = Math.Max(todaysEntry.Weight - goal.GoalWeight, 0); // prevent from going negative
            int daysSinceStart = (int)(Math.Floor((DateTime.Today.Date - goal.StartDate.Date).TotalDays));

            // ex: You have [lost/gained] [XX.X] [pounds/kilograms] since starting on your current goal [XX] days ago. You have [XX] days left to lose a remaining [XX.X] pounds. [You can do it!/Keep up the good work!]
            // NOTE: we don't want to say "-1.2 gained" so we always make sure what we display is positive
            string gainedLost = (amountLost < 0) ? Constants.Strings.DailyInfoPage_Summary_Gained : Constants.Strings.DailyInfoPage_Summary_Lost;
            string endingText = (todaysEntry.Weight <= todaysGoalWeight) ? Constants.Strings.DailyInfoPage_SummaryEnding_OnTrack : Constants.Strings.DailyInfoPage_SummaryEnding_OffTrack;

            if (goal.WeightUnit != WeightUnitEnum.StonesAndPounds)
            {
                string weightUnits = goal.WeightUnit.ToSentenceUsageName();
                return string.Format(Constants.Strings.DailyInfoPage_ProgressSummary, gainedLost, Math.Abs(amountLost), weightUnits, daysSinceStart,
                    daysToGo, amountRemaining, endingText);
            }
            else // special formatting for stones/pounds
            {
                var lostInStones = Math.Abs(amountLost).ToStonesAndPounds();
                var remainingInStones = amountRemaining.ToStonesAndPounds();
                return string.Format(Constants.Strings.DailyInfoPage_Stones_ProgressSummary, gainedLost, lostInStones.Stones, lostInStones.Pounds, daysSinceStart,
                    daysToGo, remainingInStones.Stones, remainingInStones.Pounds, endingText);
            }
        }

        public static decimal ConvertWeightUnits(decimal weightValue, WeightUnitEnum origUnits, WeightUnitEnum newUnits)
        {
            if (origUnits == newUnits)
                return weightValue;

            // we treat pounds and stones/pounds the same (just changes in how shown on graph, text, etc. - stored the same)
            if (origUnits == WeightUnitEnum.ImperialPounds && newUnits == WeightUnitEnum.StonesAndPounds ||
                origUnits == WeightUnitEnum.StonesAndPounds && newUnits == WeightUnitEnum.ImperialPounds)
                return weightValue;

            if ((origUnits == WeightUnitEnum.ImperialPounds || origUnits == WeightUnitEnum.StonesAndPounds) && newUnits == WeightUnitEnum.Kilograms)
            {
                return weightValue * Constants.App.PoundsToKilograms;
            }
            else if (origUnits == WeightUnitEnum.Kilograms && (newUnits == WeightUnitEnum.ImperialPounds || newUnits == WeightUnitEnum.StonesAndPounds))
            {
                return weightValue * Constants.App.KilogramsToPounds;
            }
            else // unknown conversion, do nothing
            {
#if DEBUG
                Debugger.Break();
#endif
                return weightValue;
            }
        }
    }
}