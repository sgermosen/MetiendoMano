using System;
using System.Globalization;
using Xamarin.Forms;

namespace ExpensesPredictor.Mobile.Converters
{
    public class NegatedBoolConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boovalue = (bool) value;
            return !boovalue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boovalue = (bool)value;
            return !boovalue;
        }
    }
}
