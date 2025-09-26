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
        /// <para>
        /// For string values, returns true if the value is equal to <see cref="bool.TrueString"/> (case-insensitive);
        /// returns false if the value is equal to <see cref="bool.FalseString"/> (case-insensitive).
        /// </para>
        /// <para>
        /// For numeric string values, returns false if the value is "0"; returns true for any other non-zero number.
        /// </para>
        /// <para>
        /// Returns false for null or any other string.
        /// </para>
        /// </returns>
        public static bool ToBoolean(this string? s)
        {
            if (bool.TryParse(s, out bool result))
            {
                return result;
            }

            if (double.TryParse(s, out double d))
            {
                return d != 0;
            }

            return false;
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

            if (int.TryParse(value, NumberStyles.Integer, provider ?? CultureInfo.InvariantCulture, out int result))
            {
                return result;
            }

            return null;
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

            if (long.TryParse(value, NumberStyles.Integer, provider ?? CultureInfo.InvariantCulture, out long result))
            {
                return result;
            }

            return null;
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

            if (double.TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, provider ?? CultureInfo.InvariantCulture, out double result))
            {
                return result;
            }

            return null;
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

            if (decimal.TryParse(value, NumberStyles.Number, provider ?? CultureInfo.InvariantCulture, out decimal result))
            {
                return result;
            }

            return null;
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

            if (DateTime.TryParse(value, provider ?? CultureInfo.InvariantCulture, styles ?? DateTimeStyles.None, out DateTime result))
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Converts the specified string representation of a date and time to an equivalent date and time with an offset, using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="value">A string that contains a date and time to convert.</param>
        /// <param name="styles">
        /// A bitwise combination of enumeration values that defines how to interpret the parsed date in relation to the current time zone or the current date.
        /// A typical value to specify is <see cref="DateTimeStyles.None"/>.
        /// </param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information. Default is <see cref="CultureInfo.InvariantCulture"/></param>
        /// <returns>
        /// The date and time with an offset equivalent of the value of value, or null if:
        /// - value is null, empty, or contains only whitespace
        /// - value is not in the correct format
        /// - value represents a date and time less than <see cref="DateTimeOffset.MinValue"/> or greater than <see cref="DateTimeOffset.MaxValue"/>
        /// </returns>
        /// <remarks>
        /// This method uses <see cref="DateTimeOffset.TryParse"/>. If the input string <paramref name="value"/> contains offset information, it is used.
        /// If no offset information is present, the offset is determined by the <paramref name="styles"/> parameter.
        /// If <paramref name="styles"/> is <see cref="DateTimeStyles.None"/>, the local system's time zone offset is used.
        /// </remarks>
        public static DateTimeOffset? ToDateTimeOffset(this string? value, DateTimeStyles? styles = DateTimeStyles.None, IFormatProvider? provider = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            if (DateTimeOffset.TryParse(value, provider ?? CultureInfo.InvariantCulture, styles ?? DateTimeStyles.None, out DateTimeOffset result))
            {
                return result;
            }

            return null;
        }

#if NET5_0_OR_GREATER
        /// <summary>
        /// Converts the specified string representation to an equivalent <see cref="DateOnly"/>.
        /// </summary>
        /// <param name="value">A string that contains a date to convert.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information. Default is <see cref="CultureInfo.InvariantCulture"/></param>
        /// <returns>A <see cref="DateOnly"/> equivalent of the value, or null if conversion fails.</returns>
        /// <remarks>If the string contains time information, it will be omitted.</remarks>
        public static DateOnly? ToDateOnly(this string? value, IFormatProvider? provider = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            provider ??= CultureInfo.InvariantCulture;
            if (DateOnly.TryParse(value, provider, DateTimeStyles.None, out DateOnly dateOnly))
            {
                return dateOnly;
            }
            else if (DateTime.TryParse(value, provider, DateTimeStyles.None, out DateTime dateTime))
            {
                return DateOnly.FromDateTime(dateTime);
            }

            return null;
        }
#endif

        /// <summary>
        /// Converts the specified string representation to an equivalent enumeration type.
        /// </summary>
        /// <typeparam name="T">An enumeration type.</typeparam>
        /// <param name="value">A string that contains the name or numeric value of one or more enumerated constants to convert.</param>
        /// <returns>An object of type T whose value is represented by value, or null if conversion fails.</returns>
        /// <remarks>The result is not validated against a defined member.</remarks>
        public static T? ToEnum<T>(this string? value) where T : struct, Enum
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            if (Enum.TryParse(value, true, out T result))
            {
                return result;
            }

            return null;
        }
    }
}
