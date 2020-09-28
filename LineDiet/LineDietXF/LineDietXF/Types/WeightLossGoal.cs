using LineDietXF.Enumerations;
using SQLite;
using System;

namespace LineDietXF.Types
{
    /// <summary>
    /// The WeightLossGoal object is one of the main data types of the app including information about the user's weight loss goal.
    /// NOTE:: There can only be 1 instance of a WeightLossGoal at any given time
    /// NOTE:: includes attributes necessary by SQLite for storage to SQL table
    /// </summary>
    [Table("WeightLossGoal")]
    public class WeightLossGoal
    {
        [PrimaryKey, Unique, NotNull]
        public DateTime StartDate { get; set; }

        [NotNull]
        public decimal StartWeight { get; set; }

        [NotNull]
        public DateTime GoalDate { get; set; }

        [NotNull]
        public decimal GoalWeight { get; set; }

        [Column("Units")]
        public WeightUnitEnum WeightUnit { get; set; }

        public WeightLossGoal()
        {
            // NOTE:: The original database did not have this field, and SQLite-Net does not support Default attributes, so we make sure
            // we explicitly set this here to the default we want for pre-existing records before this field was added
            WeightUnit = WeightUnitEnum.ImperialPounds;
        }

        public WeightLossGoal(DateTime startDate, decimal startWeight, DateTime goalDate, decimal goalWeight, WeightUnitEnum units)
        {
            StartDate = startDate;
            StartWeight = startWeight;
            GoalDate = goalDate;
            GoalWeight = goalWeight;
            WeightUnit = units;
        }
    }
}