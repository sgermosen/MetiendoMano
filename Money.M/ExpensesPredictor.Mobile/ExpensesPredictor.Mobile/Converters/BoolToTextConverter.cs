using System;
using System.Globalization;
using Xamarin.Forms;

namespace ExpensesPredictor.Mobile.Converters
{
    public class BoolToTextConverter: IValueConverter
    {
        public string TrueText { get; set; }
        public string FalseText { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (bool) value ? TrueText : FalseText;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
