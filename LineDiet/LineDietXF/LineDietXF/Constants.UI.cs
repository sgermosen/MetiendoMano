using Plugin.Share.Abstractions;
using Xamarin.Forms;

namespace LineDietXF.Constants
{
    public static class UI
    {
        public const int DailyInfoFontSize_Normal = 72;
        public const int DailyInfoFontSize_Stones = 48;
        public const int ListingFontSize_Normal = 24;
        public const int ListingFontSize_Stones = 20;

        public static Color LightGrayFillColor = Color.FromRgb(128, 128, 128);
        public static Color DarkGrayFillColor = Color.FromRgb(64, 64, 64);
        public static Color LightGreenFillColor = Color.FromRgb(0, 212, 35);
        public static Color DarkGreenFillColor = Color.FromRgb(0, 170, 28);
        public static Color LightRedFillColor = Color.FromRgb(223, 26, 8);
        public static Color DarkRedFillColor = Color.FromRgb(200, 23, 7);

        public static Color GraphMinorGridLinesColor = Color.FromRgba(255, 255, 255, 24);
        public static Color GraphMajorGridLinesColor = Color.FromRgba(255, 255, 255, 64);
        public static Color GraphBorderColor = Color.FromRgba(255, 255, 255, 96);
        public const double GraphMarkerSize = 4; // size of dot markers
        public const double GraphStrokeThickness = 4; // size of lines between dots

        public static Color WebBrowserNavColor = Color.FromRgb(64, 64, 64);
    }
}