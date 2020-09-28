using LineDietXF.Types;
using System;

namespace LineDietXF.Extensions
{
    public static class StonesAndPoundsExtensions
    {
        public static StonesAndPounds ToStonesAndPounds(this decimal weightInPounds)
        {
            int stones = Convert.ToInt32(Math.Truncate(weightInPounds / Constants.App.PoundsInAStone));
            decimal pounds = weightInPounds % Constants.App.PoundsInAStone;

            return new StonesAndPounds(stones, pounds);
        }

        public static decimal ToStonesDecimal(this decimal weightInPounds)
        {
            return weightInPounds / Constants.App.PoundsInAStone;
        }

        public static decimal ToPoundsDecimal(this StonesAndPounds stonesPounds)
        {
            return (stonesPounds.Stones * Constants.App.PoundsInAStone) + stonesPounds.Pounds;
        }

        // Turns something like "1.1" into "1st 1.1lbs"
        public static string ToLongStonesPoundsString(this decimal stonesDecimal)
        {
            var stonesAndPounds = stonesDecimal.ToStonesAndPounds();
            return string.Format(Constants.Strings.Common_Stones_WeightFormat, stonesAndPounds.Stones, stonesAndPounds.Pounds);
        }

        public static string PoundsToShortStonesDecimalString(this decimal weightInPounds)
        {
            var weightInStones = weightInPounds.ToStonesDecimal();
            return string.Format(Constants.Strings.Common_Stones_ShortWeightFormat, weightInStones);
        }
    }
}