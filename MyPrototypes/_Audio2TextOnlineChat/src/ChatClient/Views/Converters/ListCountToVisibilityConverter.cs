using System;
using System.Collections;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatClient.Views.Converters
{
    class ListCountToVisibilityConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is IList enumerable))
                return false;

            return enumerable.Count == 0;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => null;

        public object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}
