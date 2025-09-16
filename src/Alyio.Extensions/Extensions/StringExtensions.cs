// MIT License

namespace Alyio.Extensions
{
    /// <summary>
    /// Extension methods for converting a <see cref="string"/> type to another base data type.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts the specified string representation of a logical value to its <see cref="bool"/> equivalent.
        /// </summary>
        /// <param name="s">A string containing the value to convert.</param>
        /// <returns>
        /// true if value is equal to <see cref="bool.TrueString"/> (case-insensitive);
        /// false if value is equal to <see cref="bool.FalseString"/> (case-insensitive) or null;
        /// otherwise false.
        /// </returns>
        public static bool ToBoolean(this string? s)
        {
            return !string.IsNullOrWhiteSpace(s) && bool.TryParse(s, out bool result) && result;
        }

        /// <summary>
        /// Converts the specified string representation of a number to an equivalent 32-bit signed integer.
        /// </summary>
        /// <param name="value">A string that contains a number to convert.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information. Default is <see cref="CultureInfo.InvariantCulture"/></param>
        /// <returns>
        /// A 32-bit signed integer that is equivalent to the number in value, or null if:
        /// - value is null, empty, or contains only whitespace
        /// - value is not in the correct format
        /// - value represents a number less than <see cref="int.MinValue"/> or greater than <see cref="int.MaxValue"/>
        /// </returns>
        public static int? ToInt32(this string? value, IFormatProvider? provider = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            try
            {
                return int.Parse(value, provider ?? CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Converts the specified string representation of a number to an equivalent 64-bit signed integer.
        /// </summary>
        /// <param name="value">A string that contains a number to convert.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information. Default is <see cref="CultureInfo.InvariantCulture"/></param>
        /// <returns>
        /// A 64-bit signed integer that is equivalent to the number in value, or null if:
        /// - value is null, empty, or contains only whitespace
        /// - value is not in the correct format
        /// - value represents a number less than <see cref="long.MinValue"/> or greater than <see cref="long.MaxValue"/>
        /// </returns>
        public static long? ToInt64(this string? value, IFormatProvider? provider = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            try
            {
                return long.Parse(value, provider ?? CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Converts the specified string representation of a number to an equivalent double-precision floating-point number.
        /// </summary>
        /// <param name="value">A string that contains a number to convert.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information. Default is <see cref="CultureInfo.InvariantCulture"/></param>
        /// <returns>
        /// A double-precision floating-point number that is equivalent to the number in value, or null if:
        /// - value is null, empty, or contains only whitespace
        /// - value is not in the correct format
        /// - value represents a number less than <see cref="double.MinValue"/> or greater than <see cref="double.MaxValue"/>
        /// </returns>
        public static double? ToDouble(this string? value, IFormatProvider? provider = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            try
            {
                return double.Parse(value, provider ?? CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Converts the specified string representation of a number to an equivalent decimal number.
        /// </summary>
        /// <param name="value">A string that contains a number to convert.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information. Default is <see cref="CultureInfo.InvariantCulture"/></param>
        /// <returns>
        /// A decimal number that is equivalent to the number in value, or null if:
        /// - value is null, empty, or contains only whitespace
        /// - value is not in the correct format
        /// - value represents a number less than <see cref="decimal.MinValue"/> or greater than <see cref="decimal.MaxValue"/>
        /// </returns>
        public static decimal? ToDecimal(this string? value, IFormatProvider? provider = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            try
            {
                return decimal.Parse(value, provider ?? CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Converts the specified string representation of a date and time to an equivalent date and time, using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="value">A string that contains a date and time to convert.</param>
        /// <param name="styles">
        /// A bitwise combination of enumeration values that defines how to interpret the parsed date in relation to the current time zone or the current date.
        /// A typical value to specify is <see cref="DateTimeStyles.None"/>.
        /// </param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information. Default is <see cref="CultureInfo.InvariantCulture"/></param>
        /// <returns>
        /// The date and time equivalent of the value of value, or null if:
        /// - value is null, empty, or contains only whitespace
        /// - value is not in the correct format
        /// - value represents a date and time less than <see cref="DateTime.MinValue"/> or greater than <see cref="DateTime.MaxValue"/>
        /// </returns>
        public static DateTime? ToDateTime(this string? value, DateTimeStyles? styles = DateTimeStyles.None, IFormatProvider? provider = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            try
            {
                return DateTime.Parse(value, provider ?? CultureInfo.InvariantCulture, styles ?? DateTimeStyles.None);
            }
            catch
            {
                return null;
            }
        }
    }
}
