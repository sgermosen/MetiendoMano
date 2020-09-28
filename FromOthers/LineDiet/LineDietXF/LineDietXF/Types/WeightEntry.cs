using LineDietXF.Enumerations;
using LineDietXF.Extensions;
using LineDietXF.Helpers;
using SQLite;
using System;
using System.Diagnostics;

namespace LineDietXF.Types
{
    /// <summary>
    /// The WeightEntry object is one of the main data types of the app including information about a specific weight entry
    /// NOTE:: includes attributes necessary by SQLite for storage to SQL table
    /// </summary>
    [Table("WeightEntries")]
    public class WeightEntry
    {
        [PrimaryKey, Unique, NotNull]
        public DateTime Date { get; set; }

        [NotNull]
        public decimal Weight { get; set; }
        
        [Column("Units")]
        public WeightUnitEnum WeightUnit { get; set; }

        public WeightEntry()
        {
            Weight = decimal.MinValue;

            // NOTE:: The original database did not have this field, and SQLite-Net does not support Default attributes, so we make sure
            // we explicitly set this here to the default we want for pre-existing records before this field was added
            WeightUnit = WeightUnitEnum.ImperialPounds;
        }

        public WeightEntry(DateTime dt, decimal wt, WeightUnitEnum weightUnit)
        {
            Date = dt;
            Weight = wt;
            WeightUnit = weightUnit;
        }

        public override string ToString()
        {
            if (Weight == decimal.MinValue)
            {
                if (Debugger.IsAttached)
                    Debugger.Break();                

                return "<error>";
            }

            if (WeightUnit == WeightUnitEnum.StonesAndPounds)
                return Weight.ToLongStonesPoundsString();
            else
                return string.Format(Constants.Strings.Common_WeightFormat, Weight);
        }
    }
}