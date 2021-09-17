using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatClient.Views.Converters
{
    class TextToColorConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case string b when b == "Me":
                    return Color.LawnGreen;
                case string n when !string.IsNullOrWhiteSpace(n):
                    return Color.DeepSkyBlue;
                case string a when string.IsNullOrWhiteSpace(a):
                default:
                    return Color.DarkGray;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => null;

        public object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}
