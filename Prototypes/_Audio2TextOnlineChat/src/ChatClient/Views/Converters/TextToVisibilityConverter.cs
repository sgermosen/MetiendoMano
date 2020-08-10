using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatClient.Views.Converters
{
    class TextToVisibilityConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => !string.IsNullOrWhiteSpace((string)value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => null;

        public object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}
