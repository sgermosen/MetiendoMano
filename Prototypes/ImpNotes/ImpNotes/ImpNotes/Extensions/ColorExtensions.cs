using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ImpNotes.Extensions
{
    public static class ColorExtensions
    {
        public static string ToHex(this Color color)
        {
            int red = (int)(color.R * 255);
            int green = (int)(color.G * 255);
            int blue = (int)(color.B * 255);
            string hex = red.ToString("X2") + green.ToString("X2") + blue.ToString("X2");
            return hex;
        }
    }
}
