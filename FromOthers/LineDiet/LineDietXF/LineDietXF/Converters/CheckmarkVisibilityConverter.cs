using LineDietXF.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;

namespace LineDietXF.Converters
{
    /// <summary>
    /// Uses a static dictionary (that must be set externally) for converting a WeightEntry into whether a check mark should be shown 
    /// based on if they were on track for that day
    /// </summary>
    public class CheckmarkVisibilityConverter : IValueConverter
    {
        public static Dictionary<WeightEntry, bool> SuccessForDateLookup { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var weightEntry = value as WeightEntry;
            if (weightEntry == null)
                return true;

            if (SuccessForDateLookup == null || !SuccessForDateLookup.ContainsKey(weightEntry))
            {
                if (Debugger.IsAttached)
                    Debugger.Break(); // must set the lookup dictionary before using converter, and it must contain each weight entry

                return false;
            }

            return SuccessForDateLookup[weightEntry];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}