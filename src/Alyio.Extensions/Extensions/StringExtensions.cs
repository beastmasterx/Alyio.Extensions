using System;
using System.Globalization;

namespace Alyio.Extensions
{
    /// <summary>
    /// Extension methods for converting a <see cref="string"/> type to another base data type.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts the specified string representation of a logical value to its <see cref="System.Boolean"/> equivalent.
        /// </summary>
        /// <param name="s">A string containing the value to convert.</param>
        /// <returns>true if value is equal to <see cref="System.Boolean.TrueString"/> or false if value is equal to <see cref="System.Boolean.FalseString"/>, otherwise false.</returns>
        public static bool ToBoolean(this string? s)
        {
            return bool.TryParse(s, out bool result) && result;
        }

        /// <summary>
        /// Converts the specified string representation of a number to an equivalent 32-bit signed integer.
        /// </summary>
        /// <param name="value">A string that contains a number to convert.</param>
        /// <returns>A 32-bit signed integer that is equivalent to the number in value, or null if value is null or was not converted successfully.</returns>
        public static int? ToInt32(this string? value)
        {
            return int.TryParse(value, out int result) ? result : null;
        }

        /// <summary>
        /// Converts the specified string representation of a number to an equivalent 32-bit signed integer.
        /// </summary>
        /// <param name="value">A string that contains a number to convert.</param>
        /// <returns>A 64-bit signed integer that is equivalent to the number in value, or null if value is null or was not converted successfully.</returns>
        public static long? ToInt64(this string? value)
        {
            return long.TryParse(value, out long result) ? result : null;
        }

        /// <summary>
        /// Converts the specified string representation of a number to an equivalent double-precision floating-point number.
        /// </summary>
        /// <param name="value">A string that contains a number to convert.</param>
        /// <returns>A double-precision floating-point number that is equivalent to the number in value, or null if value is null was not converted successfully.</returns>
        public static double? ToDouble(this string? value)
        {
            return double.TryParse(value, out double result) ? result : null;
        }

        /// <summary>
        /// Converts the specified string representation of a number to an equivalent decimal number.
        /// </summary>
        /// <param name="value">A string that contains a number to convert.</param>
        /// <returns>A decimal number that is equivalent to the number in value, or null if value is null or was not converted successfully.</returns>
        public static decimal? ToDecimal(this string? value)
        {
            return decimal.TryParse(value, out decimal result) ? result : null;
        }

        /// <summary>
        /// Converts the specified string representation of a date and time to an equivalent date and time, using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="value">A string that contains a date and time to convert.</param>
        /// <param name="styles">
        /// A bitwise combination of enumeration values that defines how to interpret the parsed date in relation to the current time zone or the current date.
        /// A typical value to specify is <see cref="System.Globalization.DateTimeStyles.None"/>.
        /// </param>
        /// <param name="provider">An <see cref="System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information. Default is <see cref="CultureInfo.InvariantCulture"/></param>
        /// <returns>The date and time equivalent of the value of value, or null if value is null or was not converted successfully.</returns>
        public static DateTime? ToDateTime(this string? value, DateTimeStyles? styles = DateTimeStyles.None, IFormatProvider? provider = null)
        {
            return DateTime.TryParse(value, provider ?? CultureInfo.InvariantCulture, styles ?? DateTimeStyles.None, out DateTime dateTime) ? dateTime : null;
        }
    }
}
