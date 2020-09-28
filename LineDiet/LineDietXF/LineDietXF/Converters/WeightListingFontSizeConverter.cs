using LineDietXF.Enumerations;
using LineDietXF.Types;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace LineDietXF.Converters
{
    public class WeightListingFontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var weightEntry = value as WeightEntry;
            if (weightEntry == null)
                return Constants.UI.ListingFontSize_Normal;

            if (weightEntry.WeightUnit == WeightUnitEnum.StonesAndPounds)
                return Constants.UI.ListingFontSize_Stones;
            else
                return Constants.UI.ListingFontSize_Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}