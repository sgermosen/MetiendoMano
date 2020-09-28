using System;

namespace Common.MyExtensions
{
    public static class DateTimeExtension
    {
        public static string UseMyCustomFormat(this System.DateTime dt)
        {
            return dt.ToString("MM/dd/yyyy");
        }

        public static string UseStandardFormat(this System.DateTime dt, bool withSeparators = true)
        {
            if (withSeparators)
            {
                return dt.ToString("yyyy/MM/dd");
            }
            else
            {
                return dt.ToString("yyyyMMdd");
            }
        }

        public static string UseMyCustomFormat(this System.DateTime? dt)
        {
            return dt != null ? UseMyCustomFormat(Convert.ToDateTime(dt)) : "";
        }

        public static int HowOldAreYou(this System.DateTime dt)
        {
            var now = System.DateTime.Today;
            int age = now.Year - dt.Year;

            if (now < dt.AddYears(age)) age--;

            return age;
        }

        public static int HowOldAreYou(this System.DateTime? dt)
        {
            return dt != null ? HowOldAreYou(Convert.ToDateTime(dt)) : -1;
        }

        public static string TimeElapsedLikeToFacebook(this System.DateTime dt)
        {
            var span = System.DateTime.Now - dt;

            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return string.Format("about {0} {1} ago",
                years, years == 1 ? "year" : "years");
            }
            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days % 31 != 0)
                    months += 1;
                return string.Format("about {0} {1} ago",
                months, months == 1 ? "month" : "months");
            }
            if (span.Days > 0)
                return string.Format("about {0} {1} ago",
                span.Days, span.Days == 1 ? "day" : "days");
            if (span.Hours > 0)
                return string.Format("about {0} {1} ago",
                span.Hours, span.Hours == 1 ? "hour" : "hours");
            if (span.Minutes > 0)
                return string.Format("about {0} {1} ago",
                span.Minutes, span.Minutes == 1 ? "minute" : "minutes");
            if (span.Seconds > 5)
                return string.Format("about {0} seconds ago", span.Seconds);
            if (span.Seconds <= 5)
                return "just now";

            return string.Empty;
        }
    }
}
