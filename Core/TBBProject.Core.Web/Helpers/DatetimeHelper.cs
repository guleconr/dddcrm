using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TBBProject.Core.Web
{
    public static class DatetimeHelper
    {
        public static List<WeekDateViewModel> GetWeeksInYear(int year)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            DateTime date1 = new DateTime(year, 12, 31);
            Calendar cal = dfi.Calendar;
            var weekCount = cal.GetWeekOfYear(date1, dfi.CalendarWeekRule,
                                                dfi.FirstDayOfWeek);
            List<WeekDateViewModel> result = new List<WeekDateViewModel>();

            for (var i = 1; i <= weekCount; i++)
            {
                WeekDateViewModel addedDate = new WeekDateViewModel
                {
                    Year = year,
                    Week = i,
                    Text = i + "(" + FirstDateOfWeekISO8601(year, i).ToString("dd.MM.yyyy") + "/" + FirstDateOfWeekISO8601(year, i).AddDays(6).ToString("dd.MM.yyyy") + ")"
                };
                result.Add(addedDate);
            }
            return result;

        }

        public static DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            var weekNum = weekOfYear;
            if (firstWeek == 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(-3);
        }
        public static int WeekOfYearISO8601(DateTime date)
        {
            var day = (int)CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(date);
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date.AddDays(4 - ( day == 0 ? 7 : day )), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

    }
    public class WeekDateViewModel
    {
        public string Text { get; set; }
        public int Year { get; set; }
        public int Week { get; set; }
    }
}
