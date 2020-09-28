using System;

namespace LineDietXF.Constants
{
    public static class Strings
    {
        // Generic strings
        public const string Generic_OK = "OK";
        public const string Generic_Cancel = "Cancel";

        // Common strings - used multiple places
        public const string Common_WeightUnit_ImperialPounds = "pounds";
        public const string Common_WeightUnit_Kilograms = "kilograms";
        public const string Common_WeightUnit_Stones = "stones";
        public const string Common_WeightFormat = "{0:0.0}";
        public const string Common_Stones_WeightFormat = "{0:0}st {1:0.#}lbs";
        public const string Common_Stones_ShortWeightFormat = "{0:0.#}st";
        public const string Common_UpdateExistingWeight_Title = "Update existing entry?";
        public const string Common_UpdateExistingWeight_Message = "The existing weight of {0:0.0} on {1:MM/dd/yyyy} will be updated with the weight of {2:0.0}.";
        public const string Common_SaveError = "Error while saving";
        public const string Common_FatalError = "Fatal Error";
        public const string Common_SettingWeightUnits_Title = "Weight Unit Setting";
        public const string Common_SettingWeightUnits_Message = "Line Diet is currently set to use {0} for weight measurements. You can change this by going to the menu tab, and selecting Settings.";

        // Getting Started
        public static readonly string GettingStarted_Page1Title = $"Welcome to {Environment.NewLine} Line Diet!";
        public const string GettingStarted_Page1Text = "Losing weight doesn't have to be complicated. Line Diet makes losing weight simple.";
        public const string GettingStarted_Page2Title = "Set a Goal";
        public const string GettingStarted_Page2Text = "The first thing you'll do is set a goal weight and a goal date to achieve it by.";
        public const string GettingStarted_Page3Title = "Enter Weight Daily";
        public const string GettingStarted_Page3Text = "Enter your weight each morning. If you see green then eat normal. If you see red then eat light or get some exercise.";
        public const string GettingStarted_Page4Title = "Stay on Track!";
        public const string GettingStarted_Page4Text = "Adjust how hard you diet daily to stay on track and achieve your weight loss goals!";

        // Daily Info Tab
        public const string DailyInfoPage_UnknownWeight = "???.?";
        public const string DailyInfoPage_Loading = "loading..";
        public const string DailyInfoPage_HowToEat_OnDiet = "Eat light today";
        public const string DailyInfoPage_HowToEat_OffDiet = "Eat normal today";
        public const string DailyInfoPage_TodaysMessage_NoGoal = "You haven't set a goal yet, let's set one now!";
        public const string DailyInfoPage_TodaysMessage_NewGoal = "You've started your new diet, come back every morning to track your weight and see if you're on a diet that day or can have a little fun.";
        public const string DailyInfoPage_TodaysMessage_NoWeightToday = "Time to enter today's weight!";
        public const string DailyInfoPage_GoalEnd_Success = "You've reached your goal! You can continue to use Line Diet to track your maintenance of your weight, or set a new goal. Congratulations!";
        public const string DailyInfoPage_GoalEnd_Failure = "It looks like you've fallen short of your goal. Why not set a new goal and try again?";
        public const string DailyInfoPage_ProgressSummary = "You have {0} {1:0.0} {2} since starting your current goal {3} days ago. You have {4} days left to lose {5:0.0} {2}. {6}";
        public const string DailyInfoPage_Stones_ProgressSummary = "You have {0} {1:0} stone(s) {2:0.0} pounds, since starting your current goal {3} days ago. You have {4} days left to lose {5:0} stone(s), {6:0.0} pounds. {7}";
        public const string DailyInfoPage_Summary_Gained = "gained";
        public const string DailyInfoPage_Summary_Lost = "lost";
        public const string DailyInfoPage_SummaryEnding_OnTrack = "Keep up the good work!";
        public const string DailyInfoPage_SummaryEnding_OffTrack = "You can do it!";

        // Graph/Listing Tab
        public const string GraphPage_ConfirmDelete_Title = "Delete entry?";
        public const string GraphPage_ConfirmDelete_Message = "Delete the entry for {0:MM/dd/yyyy}?";
        public const string GraphPage_DeleteFailed_Title = "Delete failed";
        public const string GraphPage_DeleteFailed_Message = "Deletion failed, try again.";
        public const string GraphPage_PlaceholderText_Loading = "loading..";
        public const string GraphPage_PlaceholderText_NoEntries = "No weight entries to show";

        // Main Menu Tab
        public const string Menu_GettingStarted = "Getting Started";
        public const string Menu_SetGoal = "Set Goal";
        public const string Menu_Settings = "Settings";
        public const string Menu_Share = "Share";
        public const string Menu_LeaveAReview = "Leave a Review";
        public const string Menu_SendFeedback = "Send Feedback";
        public const string Menu_About = "About";
        public const string Menu_Debug = "Debug Menu";
        public const string Review_Failed_Title = "Leave Review failed";
        public const string Review_Failed_Message = "Unable to launch reviewing of the app. Please visit {0} instead and leave a review. Thanks much!";
        public const string Review_Failed_Part_Android = "Google Play";
        public const string Review_Failed_Part_iOS = "the iTunes App Store";
        public const string Review_Failed_Part_Generic = "the app store";

        // Settings Page
        public const string Settings_WeightUnits = "Measurement Units: ";
        public const string Settings_ImperialPound = "Imperial Pound (US)";
        public const string Settings_Kilograms = "Kilograms";
        public const string Settings_StonesAndPounds = "Stones";
        public const string Settings_ChangeWeightUnitsActionSheet = "Select the weight units to use";
        public const string Settings_ChangeWeightUnits_GetWeightsError_Title = "Unable to get weights";
        public const string Settings_ChangeWeightUnits_GetWeightsError_Message = "An error occurred trying to retrieve your weights, cannot continue.";        
        public const string Settings_ChangeWeightUnits_ConvertWarning = "Convert weights to new units (ex: 100.0 pounds to 45.36 kilos)?";
        public const string Settings_ChangeWeightUnits_ConvertWarning_ConvertWeightValues = "Convert weight values";
        public const string Settings_ChangeWeightUnits_ConvertWarning_ChangeUnits = "Just change units";
        public const string Settings_ChangeWeightUnits_ConvertWeightValues_FatalError = "A fatal error occurred converting weight values, please contact support at smartypantscoding@gmail.com. Details: ";
        public const string Settings_ChangeWeightUnits_ChangeUnits_FatalError = "A fatal error occurred changing weight units, please contact support at smartypantscoding@gmail.com. Details: ";

        // Data Services
        public const string DataService_ChangeWeightAndGoalUnits_UnableToGetWeights = "Unable to get weights, cannot continue.";
        public const string DataService_ChangeWeightAndGoalUnits_FailedRemovingWeight = "Unable to update weight entry for {0}. Failed during remove.";
        public const string DataService_ChangeWeightAndGoalUnits_FailedAddingWeight = "Unable to update weight entry for {0}. Failed during add.";
        public const string DataService_ChangeWeightAndGoalUnits_FailedRemovingGoal = "Unable to update goal. Failed during remove.";
        public const string DataService_ChangeWeightAndGoalUnits_FailedAddingGoal = "Unable to update goal. Failed during add.";

        // Weight Entry Page
        public const string WeightEntryPage_Title_Add = "Enter Weight";
        public const string WeightEntryPage_Title_Update = "Update Weight";
        public const string WeightEntryPage_InvalidWeight_Title = "Invalid weight";
        public const string WeightEntryPage_InvalidWeight_Message = "Could not convert entered weight to a valid weight.";
        public const string WeightEntryPage_Save_RemoveExistingWeightFailed_Message = "An error occurred removing an existing weight entry for the selected date.";
        public const string WeightEntryPage_Save_AddingWeightFailed_Message = "An error occurred when adding the new weight entry.";
        public const string WeightEntryPage_Save_Exception_Message = "An exception occurred while saving your weight entry.";
        public const string WeightEntrylPage_WeightLabel = "Weight (in {0})";

        // Set Goal Page
        public const string SetGoalPage_InvalidWeight_Title = "Invalid weight";
        public const string SetGoalPage_InvalidWeight_Message = "Could not convert entered start and/or goal weight into a valid weight.";
        public const string SetGoalPage_GoalWeightGreaterThanStartWeight_Title = "Are you sure?";
        public const string SetGoalpage_GoalWeightGreaterThanStartWeight_Message = "Your goal weight is greater than your start weight. Are you sure your goal is to gain weight?";
        public const string SetGoalPage_Save_RemoveExistingWeightFailed_Message = "An error occurred removing an existing weight entry for the goal start date.";
        public const string SetGoalPage_Save_AddingWeightFailed_Message = "An error occurred adding a new weight entry for the goal start date.";
        public const string SetGoalPage_Save_AddingGoalFailed_Message = "An error occurred creating the new weight loss goal.";
        public const string SetGoalPage_Save_Exception_Message = "An exception occurred while saving your weight loss goal.";
        public const string SetGoalPage_StartWeightLabel = "Start Weight (in {0})";
        public const string SetGoalPage_GoalWeightLabel = "Goal Weight (in {0})";

        // Sharing
        public const string ShareTitle = "Check out Line Diet!";
        public const string ShareMessage = "You should check out Line Diet. It's a free app for simple diet tracking!";
    }
}