using LineDietXF.Types;
using System;
using System.Globalization;
using Xamarin.Forms;

/// <summary>
/// Converts a WeightEntry to a string representation of the weight value
/// </summary>
namespace LineDietXF.Converters
{
    public class WeightValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var weightEntry = value as WeightEntry;
            if (weightEntry == null)
                return "<error>";

            return weightEntry.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}