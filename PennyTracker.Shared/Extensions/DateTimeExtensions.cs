using System;

namespace PennyTracker.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        private const int DAYS_OF_WEEK = 7;

        public static DateTime ToDateTime(this long unixTime)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTime);
            return dtDateTime;
        }

        public static long ToUnixTime(this DateTime datetime)
        {
            TimeSpan timeSpan = datetime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (long)timeSpan.TotalSeconds;
        }

        public static DateTime StartOfWeek(this DateTime dateTime, DayOfWeek startOfWeek)
        {
            int diff = (DAYS_OF_WEEK + (dateTime.DayOfWeek - startOfWeek)) % DAYS_OF_WEEK;

            return dateTime.AddDays(-1 * diff).Date;
        }

        public static DateTime EndOfWeek(this DateTime dateTime, DayOfWeek startOfWeek)
        {
            int diff = (DAYS_OF_WEEK + (dateTime.DayOfWeek - startOfWeek)) % DAYS_OF_WEEK;

            return dateTime.AddDays(-1 * diff).Date.AddDays(DAYS_OF_WEEK);
        }

        public static DateTime StartOfLastWeek(this DateTime dateTime, DayOfWeek startOfWeek)
        {
            int diff = (DAYS_OF_WEEK + (dateTime.DayOfWeek - startOfWeek)) % DAYS_OF_WEEK;

            return dateTime.AddDays(diff).Date;
        }

        public static DateTime EndOfLastWeek(this DateTime dateTime, DayOfWeek startOfWeek)
        {
            int diff = (DAYS_OF_WEEK + (dateTime.DayOfWeek - startOfWeek)) % DAYS_OF_WEEK;
            return dateTime.AddDays(-1 * diff).Date.AddDays(DAYS_OF_WEEK);
        }

        public static DateTime StartOfMonth(this DateTime dateTime) => 
            new DateTime(dateTime.Year, dateTime.Month, 1);

        public static DateTime EndOfMonth(this DateTime dateTime) =>
            new DateTime(dateTime.Year, dateTime.AddMonths(1).Month, 1);

        public static DateTime StartOfLastMonth(this DateTime dateTime) =>
            new DateTime(dateTime.Year, dateTime.AddMonths(-1).Month, 1);
    }
}
