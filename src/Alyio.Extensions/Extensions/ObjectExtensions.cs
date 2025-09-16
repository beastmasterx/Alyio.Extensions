// MIT License

namespace Alyio.Extensions
{
    /// <summary>
    /// Extension methods for converting a <see cref="object"/> type to another base data type.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Converts the value of a specified object to an equivalent <see cref="bool"/> value.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information. Default is <see cref="CultureInfo.InvariantCulture"/></param>
        /// <returns>
        /// - false if value is null.
        /// - true or false, which reflects the value returned by invoking the <see cref="IConvertible.ToBoolean"/> method for the underlying type of value.
        /// - true if value is not zero; otherwise, false.
        /// - true if value equals <see cref="bool.TrueString"/>, or false if value equals <see cref="bool.FalseString"/> or null.
        /// </returns>
        public static bool ToBoolean(this object? value, IFormatProvider? provider = null)
        {
            provider ??= CultureInfo.InvariantCulture;

            if (value == null)
            {
                return false;
            }

            if (value is bool boolValue)
            {
                return boolValue;
            }

            if (value is IConvertible converter)
            {
                try
                {
                    return converter.ToBoolean(provider);
                }
                catch
                {
                    if (value.ToDouble(provider) is double d1)
                    {
                        return d1 != 0D;
                    }
                }
            }

            return value.ToDouble(provider) is double d2 ? d2 != 0D : value.ToString()?.ToBoolean() ?? false;
        }

        /// <summary>
        /// Converts the value of the specified object to its equivalent string representation.
        /// </summary>
        /// <param name="value">An object that supplies the value to convert, or null.</param>
        /// <returns>The string representation of value, or <see cref="string.Empty"/> if value is null.</returns>
        [Obsolete("This method has been renamed to 'ToStringOrEmpty' for better clarity. 'ToStringExt' will be removed in a future version.")]
        public static string ToStringExt(this object? value) => ToStringOrEmpty(value);

        /// <summary>
        /// Converts the value of the specified object to its equivalent string representation.
        /// </summary>
        /// <param name="value">An object that supplies the value to convert, or null.</param>
        /// <returns>The string representation of value, or <see cref="string.Empty"/> if value is null.</returns>
        public static string ToStringOrEmpty(this object? value) => value?.ToString() ?? string.Empty;

        /// <summary>
        /// Converts the value of the specified object to a 32-bit signed integer.
        /// </summary>
        /// <param name="value">An object that supplies the value to convert, or null.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information. Default is <see cref="CultureInfo.InvariantCulture"/></param>
        /// <returns>A 32-bit signed integer equivalent to value, or null if value is null or was not converted successfully.</returns>
        public static int? ToInt32(this object? value, IFormatProvider? provider = null)
        {
            provider ??= CultureInfo.InvariantCulture;

            if (value == null)
            {
                return null;
            }

            if (value is int intValue)
            {
                return intValue;
            }

            if (value is IConvertible converter)
            {
                try
                {
                    return converter.ToInt32(provider);
                }
                catch
                {
                    return value.ToString()?.ToInt32(provider);
                }
            }

            return value.ToString()?.ToInt32(provider);
        }

        /// <summary>
        /// Converts the value of the specified object to a 64-bit signed integer.
        /// </summary>
        /// <param name="value">An object that supplies the value to convert, or null.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information. Default is <see cref="CultureInfo.InvariantCulture"/></param>
        /// <returns>A 64-bit signed integer equivalent to value, or null if value is null or was not converted successfully.</returns>
        public static long? ToInt64(this object? value, IFormatProvider? provider = null)
        {
            provider ??= CultureInfo.InvariantCulture;

            if (value == null)
            {
                return null;
            }

            if (value is long longValue)
            {
                return longValue;
            }

            if (value is IConvertible converter)
            {
                try
                {
                    return converter.ToInt64(provider);
                }
                catch
                {
                    return value.ToString()?.ToInt64(provider);
                }
            }

            return value.ToString()?.ToInt64(provider);
        }

        /// <summary>
        /// Converts the value of the specified object to a double-precision floating-point number.
        /// </summary>
        /// <param name="value">An object that supplies the value to convert, or null.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information. Default is <see cref="CultureInfo.InvariantCulture"/></param>
        /// <returns>A double-precision floating-point number equivalent to value, or null if value is null or was not converted successfully.</returns>
        public static double? ToDouble(this object? value, IFormatProvider? provider = null)
        {
            provider ??= CultureInfo.InvariantCulture;

            if (value == null)
            {
                return null;
            }

            if (value is double doubleValue)
            {
                return doubleValue;
            }

            if (value is IConvertible converter)
            {
                try
                {
                    return converter.ToDouble(provider);
                }
                catch
                {
                    return value.ToString()?.ToDouble(provider);
                }
            }

            return value.ToString()?.ToDouble(provider);
        }

        /// <summary>
        /// Converts the value of the specified object to a decimal number.
        /// </summary>
        /// <param name="value">An object that supplies the value to convert, or null.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information. Default is <see cref="CultureInfo.InvariantCulture"/></param>
        /// <returns>A decimal number equivalent to value, or null if value is null or was not converted successfully.</returns>
        public static decimal? ToDecimal(this object? value, IFormatProvider? provider = null)
        {
            provider ??= CultureInfo.InvariantCulture;

            if (value == null)
            {
                return null;
            }

            if (value is decimal decimalValue)
            {
                return decimalValue;
            }

            if (value is IConvertible converter)
            {
                try
                {
                    return converter.ToDecimal(provider);
                }
                catch
                {
                    return value.ToString()?.ToDecimal(provider);
                }
            }

            return value.ToString()?.ToDecimal(provider);
        }

        /// <summary>
        /// Converts the value of the specified object to a date and time.
        /// </summary>
        /// <param name="value">An object that supplies the value to convert, or null.</param>
        /// <param name="styles">
        /// A bitwise combination of enumeration values that defines how to interpret the parsed date in relation to the current time zone or the current date.
        /// A typical value to specify is <see cref="System.Globalization.DateTimeStyles.None"/>.
        /// </param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information. Default is <see cref="CultureInfo.InvariantCulture"/></param>
        /// <returns>The date and time equivalent of the value of value, or null if value is null or was not converted successfully.</returns>
        public static DateTime? ToDateTime(this object? value, DateTimeStyles? styles = DateTimeStyles.None, IFormatProvider? provider = null)
        {
            provider ??= CultureInfo.InvariantCulture;

            if (value == null)
            {
                return null;
            }

            if (value is DateTime dateTimeValue)
            {
                return dateTimeValue;
            }

            if (value is IConvertible converter)
            {
                try
                {
                    return converter.ToDateTime(provider);
                }
                catch
                {
                    return value.ToString()?.ToDateTime(styles, provider);
                }
            }

            return value.ToString()?.ToDateTime(styles, provider);
        }

        /// <summary>
        /// Converts the value of the specified object to a date without time.
        /// </summary>
        /// <param name="value">An object that supplies the value to convert, or null.</param>
        /// <param name="styles">
        /// A bitwise combination of enumeration values that defines how to interpret the parsed date in relation to the current time zone or the current date.
        /// A typical value to specify is <see cref="System.Globalization.DateTimeStyles.None"/>.
        /// </param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information. Default is <see cref="CultureInfo.InvariantCulture"/></param>
        /// <returns>The date without time equivalent of the value of value, or null if value is null or was not converted successfully.</returns>
        public static DateTime? ToDate(this object? value, DateTimeStyles? styles = DateTimeStyles.None, IFormatProvider? provider = null)
        {
            return value?.ToDateTime(styles, provider)?.Date;
        }

        /// <summary>
        /// Converts the value of the specified object to an enumeration type.
        /// </summary>
        /// <typeparam name="T">An enumeration type.</typeparam>
        /// <param name="value">An object that supplies the value to convert, or null.</param>
        /// <returns>An object of type enumType whose value is represented by value, or default(T) if conversion fails.</returns>
        public static T? ToEnum<T>(this object? value) where T : struct, Enum
        {
            if (value is T v)
            {
                return v;
            }

            var text = value?.ToString();

            if (Enum.TryParse(text, true, out T e))
            {
                return e;
            }

            return null;
        }
    }
}
