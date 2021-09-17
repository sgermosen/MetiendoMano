using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatClient.Views.Converters
{
    class IntToVisibilityConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is int count))
                return false;

            return count > 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => null;

        public object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}
