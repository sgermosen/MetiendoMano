using LineDietXF.Enumerations;

namespace LineDietXF.Types
{
    /// <summary>
    /// Contains all information necessary to update the UI based on the user's data
    /// </summary>
    public class InfoForToday
    {
        public string TodaysDisplayWeight { get; set; }
        public string HowToEatMessage { get; set; }
        public string TodaysMessage { get; set; }
        public BaseColorEnum ColorToShow { get; set; }
        public bool IsEnterWeightButtonVisible { get; set; }
        public bool IsSetGoalButtonVisible { get; set; }

        public InfoForToday()
        {
            ColorToShow = BaseColorEnum.Gray;
            IsEnterWeightButtonVisible = false;
            IsSetGoalButtonVisible = false;
        }
    }
}