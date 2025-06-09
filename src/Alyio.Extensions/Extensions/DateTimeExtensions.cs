using System.Globalization;

namespace Alyio.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="DateTime"/> type conversions and formatting.
    /// </summary>
    public static class DateTimeExtensions
    {
        private static readonly DateTime UnixEpochTime = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Converts the <see cref="DateTime"/> object to its equivalent integer representation using the specified 'yyyyMMdd' format.
        /// </summary>
        /// <param name="datetime">The date and time value to convert.</param>
        /// <returns>A 32-bit signed integer representing the date in 'yyyyMMdd' format.</returns>
        public static int ToDateInt32(this DateTime datetime)
        {
            return int.Parse(datetime.ToString("yyyyMMdd", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the <see cref="DateTime"/> object to its equivalent string representation using the specified 'yyyy-MM-dd HH:mm:ss' format.
        /// </summary>
        /// <param name="datetime">The date and time value to convert.</param>
        /// <returns>The string representation of the date and time in 'yyyy-MM-dd HH:mm:ss' format.</returns>
        public static string ToyyyyMMddHHmmss(this DateTime datetime)
        {
            return datetime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the <see cref="DateTime"/> object to its equivalent Unix timestamp (number of seconds since Unix epoch).
        /// </summary>
        /// <param name="datetime">The date and time value to convert.</param>
        /// <returns>A 64-bit signed integer representing the number of seconds since Unix epoch (1970-01-01 00:00:00 UTC).</returns>
        public static long ToUnix(this DateTime datetime)
        {
            var timeSpan = datetime.ToUniversalTime().Subtract(UnixEpochTime);
            return (long)timeSpan.TotalSeconds;
        }

        /// <summary>
        /// Converts a Unix timestamp (number of seconds since Unix epoch) to its equivalent <see cref="DateTime"/>.
        /// </summary>
        /// <param name="seconds">The Unix timestamp to convert.</param>
        /// <returns>
        /// A <see cref="DateTime"/> representing the local time corresponding to the given Unix timestamp, or null if:
        /// - The resulting date is outside the valid range for <see cref="DateTime"/>
        /// - The timestamp is negative
        /// </returns>
        public static DateTime? ToDateTime(this long seconds)
        {
            if (seconds < 0)
            {
                return null;
            }

            try
            {
                return UnixEpochTime.AddSeconds(seconds);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Converts a Unix timestamp (number of seconds since Unix epoch) to its equivalent <see cref="DateTime"/>.
        /// </summary>
        /// <param name="seconds">The Unix timestamp to convert.</param>
        /// <returns>
        /// A <see cref="DateTime"/> representing the local time corresponding to the given Unix timestamp, or null if:
        /// - The resulting date is outside the valid range for <see cref="DateTime"/>
        /// - The timestamp is negative
        /// </returns>
        public static DateTime? ToDateTime(this double seconds)
        {
            if (double.IsNaN(seconds) || double.IsInfinity(seconds) || seconds < 0)
            {
                return null;
            }

            try
            {
                return UnixEpochTime.AddSeconds(seconds);
            }
            catch
            {
                return null;
            }
        }
    }
}
