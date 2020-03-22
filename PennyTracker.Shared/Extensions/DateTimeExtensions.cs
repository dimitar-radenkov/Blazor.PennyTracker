using System;

namespace PennyTracker.Shared.Extensions
{
    public static class DateTimeExtensions
    {
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
    }
}
