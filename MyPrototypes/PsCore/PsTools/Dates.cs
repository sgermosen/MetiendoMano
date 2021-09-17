using System;
using System.Globalization;

namespace PsTools
{
    public class Dates
    {
        public static DateTime FormatedDateDo(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return DateTime.Today;
            }

            try
            {
                date = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString();
                return Convert.ToDateTime(date);
            }
            catch (Exception)
            {
                date = DateTime.Today.Day.ToString("00") + "/" + DateTime.Today.Month.ToString("00") + "/" + DateTime.Today.Year.ToString();
                return Convert.ToDateTime(date);
            }
        }
    }
}
