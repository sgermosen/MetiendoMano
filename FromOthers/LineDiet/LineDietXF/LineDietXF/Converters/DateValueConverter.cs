using System;
using System.Globalization;
using Xamarin.Forms;

namespace LineDietXF.Converters
{
    /// <summary>
    /// Converts a DateTime to a given format
    /// NOTE:: This converter is currently unnecessary, however is in place for supporting future cultures. Typically would just use something like the following:
    /// Text="{Binding MyDate, StringFormat='{0:HH:mm:ss}'}" />
    /// </summary>
    public class DateValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "<error>";

            if (value.GetType() != typeof(DateTime))
                return "<error>";

            return ((DateTime)value).ToString("MMM d, yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}