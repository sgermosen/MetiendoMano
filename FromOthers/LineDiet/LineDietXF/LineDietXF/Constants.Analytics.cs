namespace LineDietXF.Constants
{
    public static class Analytics
    {
        // Analytics Settings
        public const string GA_TrackingID = "UA-11111111-1";
        public const string GA_AppID = "LineDietXF";
        public const string GA_AppName = "Line Diet XF";
        public const int GA_DispatchPeriod = 15; // in seconds

        // Page Tracking
        public const string Page_Main = "MainPage";
        public const string Page_DailyInfo = "DailyInfoPage";
        public const string Page_Graph = "GraphPage";
        public const string Page_Menu = "MenuPage";
        public const string Page_WeightEntry = "WeightEntryPage";
        public const string Page_SetGoal = "SetGoalPage";
        public const string Page_GettingStarted = "GettingStartedPage";
        public const string Page_About = "AboutPage";

        // Event tracking
        public const string DailyInfoCategory = "DailyInfo";
        public const string DailyInfo_LaunchedAddWeight = "LaunchedAddWeight";
        public const string DailyInfo_LaunchedSetGoal = "LaunchedSetGoal";
        public const string DailyInfo_LaunchingGettingStarted = "LaunchingGettingStarted";

        public const string GraphAndListCategory = "GraphAndList";
        public const string GraphList_LaunchedAddWeight = "LaunchedAddWeight";
        public const string GraphList_LaunchedEditExistingWeight = "LaunchedEditExistingWeight";
        public const string GraphList_DeleteExistingWeight_Start = "DeleteExistingWeight_Start";
        public const string GraphList_DeleteExistingWeight_Confirmed = "DeleteExistingWeight_Confirmed";
        public const string GraphList_DeleteExistingWeight_Cancelled = "DeleteExistingWeight_Cancelled";
        public const string GraphList_DeleteExistingWeight_Failed = "DeleteExistingWeight_Failed";
        public const string GraphList_DeleteExistingWeight_Exception = "DeleteExistingWeight_Exception";

        public const string MenuCategory = "Menu";
        public const string Menu_GetingStarted = "GettingStarted";
        public const string Menu_SetGoal = "SetGoal";
        public const string Menu_Settings = "Settings";
        public const string Menu_Share = "Share";
        public const string Menu_LeaveAReview = "LeaveAReview";
        public const string Menu_SendFeedback = "SendFeedback";
        public const string Menu_About = "About";
        public const string Menu_Debug = "Debug";

        public const string WeightEntryCategory = "WeightEntry";
        public const string WeightEntry_AddedWeight = "AddedWeight";
        public const string WeightEntry_UpdateWeight = "UpdatedWeight";

        public const string SetGoalCategory = "SettingGoal";
        public const string SetGoal_SavedGoal = "SavedGoal";
    }
}