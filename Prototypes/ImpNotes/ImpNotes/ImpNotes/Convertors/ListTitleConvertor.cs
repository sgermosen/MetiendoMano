using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ImpNotes.Convertors
{
    public class ListTitleConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return String.Empty;

            var notes = System.Convert.ToString(value);
            var notesArray = notes.Split(' ');
            if (notesArray != null && notesArray.Any())
            {
                var updatedArray = notesArray.Take(15);

                return string.Join(" ", updatedArray);
            }

            return String.Empty;

        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
