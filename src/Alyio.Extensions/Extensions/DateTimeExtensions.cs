// MIT License

namespace Alyio.Extensions;

/// <summary>
/// Extension methods for <see cref="DateTime"/> type conversions and formatting.
/// </summary>
public static class DateTimeExtensions
{
    private static readonly DateTime s_epoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// Converts the <see cref="DateTime"/> object to its equivalent integer representation using the specified 'yyyyMMdd' format.
    /// </summary>
    /// <param name="datetime">The date and time value to convert.</param>
    /// <returns>A 32-bit signed integer representing the date in 'yyyyMMdd' format.</returns>
    public static int ToDateInt32(this DateTime datetime)
    {
        return datetime.Year * 10000 + datetime.Month * 100 + datetime.Day;
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
        var timeSpan = datetime.ToUniversalTime().Subtract(s_epoch);
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
            return s_epoch.AddSeconds(seconds);
        }
        catch (Exception ex) when (ex is ArgumentOutOfRangeException || ex is OverflowException)
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
            return s_epoch.AddSeconds(seconds);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Converts the <see cref="DateTime"/> object to its equivalent <see cref="DateTimeOffset"/> object.
    /// </summary>
    /// <param name="datetime">The date and time value to convert.</param>
    /// <returns>A <see cref="DateTimeOffset"/> object representing the date and time in the local time zone.</returns>
    /// <remarks>
    /// The resulting <see cref="DateTimeOffset"/> will use the offset specified by the <see cref="DateTime.Kind"/> property (e.g., Utc or Local)., otherwise if the <see cref="DateTime.Kind"/> is <see cref="DateTimeKind.Unspecified"/>, the local time zone offset will be applied.
    /// </remarks>
    public static DateTimeOffset ToDateTimeOffset(this DateTime datetime)
    {
        return new DateTimeOffset(datetime);
    }

    /// <summary>
    /// Converts the <see cref="DateTimeOffset"/> object to its equivalent <see cref="DateTime"/> object.
    /// </summary>
    /// <param name="datetimeOffset">The date and time value to convert.</param>
    /// <returns>A <see cref="DateTime"/> object representing the date and time in the UTC time zone.</returns>
    public static DateTime ToDateTime(this DateTimeOffset datetimeOffset)
    {
        return datetimeOffset.UtcDateTime;
    }

    /// <summary>
    /// Converts the <see cref="DateTimeOffset"/> object to its equivalent Unix timestamp (number of seconds since Unix epoch).
    /// </summary>
    /// <param name="datetimeOffset">The date and time value to convert.</param>
    /// <returns>A 64-bit signed integer representing the number of seconds since Unix epoch (1970-01-01 00:00:00 UTC).</returns>
    public static long ToUnix(this DateTimeOffset datetimeOffset)
    {
        return datetimeOffset.ToUnixTimeSeconds();
    }

    /// <summary>
    /// Converts a Unix timestamp (number of seconds since Unix epoch) to its equivalent <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <param name="seconds">The Unix timestamp to convert.</param>
    /// <returns>A <see cref="DateTimeOffset"/> object representing the date and time in the utc time zone.</returns>
    /// <remarks>
    /// The <see cref="DateTimeOffset.Offset"/> property value of the returned <see cref="DateTimeOffset"/> instance is <see cref="TimeSpan.Zero"/>, which represents Coordinated Universal Time.
    /// </remarks>
    public static DateTimeOffset ToDateTimeOffset(this long seconds)
    {
        return DateTimeOffset.FromUnixTimeSeconds(seconds);
    }
}