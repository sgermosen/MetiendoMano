using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatClient.Views.Converters
{
    class StringToIntConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string text))
                return 0;

            return text.Length;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => null;

        public object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}
