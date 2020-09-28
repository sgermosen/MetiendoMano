namespace Common
{
    public static class Parameters
    {
        public const string AppContext = "AppContext";

        #region App.parameters.config
        public static string Environment { get; set; }
        public static string ProjectName { get; set; }
        public static string ProjectVersion { get; set; }
        public static int SalesCommission { get; set; }
        public static int TeacherComission { get; set; }
        public static decimal NewUserCredits { get; set; }
        #endregion

        #region Menu Category
        public static string CategoryList = "[]";
        #endregion
    }
}
