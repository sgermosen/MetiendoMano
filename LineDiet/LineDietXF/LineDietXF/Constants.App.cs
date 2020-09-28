using LineDietXF.Enumerations;

namespace LineDietXF.Constants
{
    public static class App
    {
#if DEBUG        
        // NOTE:: DEBUG FLAGS - THESE SHOULD NEVER BE CHECKED IN AS TRUE
        public const bool DEBUG_UseMocks = false;
        public const bool DEBUG_AlwaysShowGettingStarted = false;
        public const bool DEBUG_SimulateSlowResponseTimes = false;
        public const int DEBUG_SimulatedSlowResponseTime = 1000; // in milliseconds        
#endif

        // General Constants
        public const string SQLite_DB_Filename = "Linediet.db";
        public const int PoundsInAStone = 14;
        public const decimal PoundsToKilograms = 0.45359237M; // for conversion between unit types
        public const decimal KilogramsToPounds = 2.2046226218M; // for conversion between unit types

        // Graphing / Listing Weights
        public const int WeightListingMaxCount = int.MaxValue; // NOTE:: since everything is local currently we're not significantly limiting how many weights are shown on the listing
        public const int WeightGraphingMaxCount = 1000; // should always be less than or equal to HISTORY_WeightEntryMaxCount_Listing (1000 is max tested)
        public const int WeightGraphingMaxCount_Android = 525; // should always be less than or equal to HISTORY_WeightEntryMaxCount_Listing (NOTE:: Android stops drawing lines connecting dots if over ~530)

        // Setting a Goal
        public const int DefaultGoalDateOffsetInMonths = 3; // default new goal end date to X months from now        

        // Settings Keys and Default Values
        public const string Setting_DismissedStartupView_Key = "HasDismissedStartupView";
        public const bool Setting_DismissedStartupView_Default = false;
        public const string Setting_WeightUnits_Key = "WeightUnits";        
        public const WeightUnitEnum Setting_WeightUnits_Default = WeightUnitEnum.ImperialPounds;

        // Navigation Parameters
        public const string NavParam_FromGettingStarted = "FromGettingStarted";

        // Graphing (NOTE:: constants relative to break points in graph labeling are defined within GraphPageViewModel)
        public const int Graph_MinDateRangeVisible = 5;
        public const int Graph_MaxDateRangeVisible = 365;
        public const decimal Graph_WeightRange_MinPadding = 0.98M;
        public const decimal Graph_WeightRange_MaxPadding = 1.02M;
        public const decimal Graph_WeightRange_Pounds_DefaultMin = 160;
        public const decimal Graph_WeightRange_Pounds_DefaultMax = 180;
        public const decimal Graph_WeightRange_Kilograms_DefaultMin = 72;
        public const decimal Graph_WeightRange_Kilograms_DefaultMax = 82;
        public const decimal Graph_GoalOnly_Pounds_Padding = 50.0M;
        public const decimal Graph_GoalOnly_Kilograms_Padding = 22.0M;
        public const int Graph_Pounds_MinWeightRangeVisible = 5;
        public const int Graph_Pounds_MaxWeightRangeVisible = 100;
        public const int Graph_Kilograms_MinWeightRangeVisible = 5;
        public const int Graph_Kilograms_MaxWeightRangeVisible = 45;

        // Urls
        public const string ShareUrl = "http://www.linedietapp.com/?ref=LDapp";
        public const string FeedbackUrl = "http://www.linedietapp.com/feedback";
        public const string SmartyPNetUrl = "http://www.smartyp.net";
        public const string SPCWebsiteUrl = "http://www.smartypantscoding.com";
        public const string GithubUrl = "https://github.com/SmartyP/LineDiet.XamarinForms.git";
    }
}