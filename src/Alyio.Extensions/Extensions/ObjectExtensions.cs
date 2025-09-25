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

            if (value == null) return false;
            if (value is bool boolValue) return boolValue;
            if (value is string s) return s.ToBoolean();

            try
            {
                return Convert.ToBoolean(value, provider);
            }
            catch (Exception ex) when (ex is FormatException || ex is InvalidCastException || ex is OverflowException)
            {
                // Fallback to the string extension's logic for numeric-like booleans
                return value.ToString()?.ToBoolean() ?? false;
            }
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

            if (value == null) return null;
            if (value is int intValue) return intValue;
            if (value is string s) return s.ToInt32(provider);

            try
            {
                return Convert.ToInt32(value, provider);
            }
            catch (Exception ex) when (ex is FormatException || ex is InvalidCastException || ex is OverflowException)
            {
                return null;
            }
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

            if (value == null) return null;
            if (value is long longValue) return longValue;
            if (value is string s) return s.ToInt64(provider);

            try
            {
                return Convert.ToInt64(value, provider);
            }
            catch (Exception ex) when (ex is FormatException || ex is InvalidCastException || ex is OverflowException)
            {
                return null;
            }
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

            if (value == null) return null;
            if (value is double doubleValue) return doubleValue;
            if (value is string s) return s.ToDouble(provider);

            try
            {
                return Convert.ToDouble(value, provider);
            }
            catch (Exception ex) when (ex is FormatException || ex is InvalidCastException || ex is OverflowException)
            {
                return null;
            }
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

            if (value == null) return null;
            if (value is decimal decimalValue) return decimalValue;
            if (value is string s) return s.ToDecimal(provider);

            try
            {
                return Convert.ToDecimal(value, provider);
            }
            catch (Exception ex) when (ex is FormatException || ex is InvalidCastException || ex is OverflowException)
            {
                return null;
            }
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

            if (value == null) return null;
            if (value is DateTime dateTimeValue) return dateTimeValue;
            if (value is string s) return s.ToDateTime(styles, provider);

            try
            {
                return Convert.ToDateTime(value, provider);
            }
            catch (Exception ex) when (ex is FormatException || ex is InvalidCastException || ex is OverflowException)
            {
                return null;
            }
        }

        /// <summary>
        /// Converts the value of the specified object to a date and time with an offset.
        /// </summary>
        /// <param name="value">An object that supplies the value to convert, or null.</param>
        /// <param name="styles">
        /// A bitwise combination of enumeration values that defines how to interpret the parsed date in relation to the current time zone or the current date.
        /// A typical value to specify is <see cref="System.Globalization.DateTimeStyles.None"/>.
        /// </param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information. Default is <see cref="CultureInfo.InvariantCulture"/></param>
        /// <returns>The date and time with an offset equivalent of the value of value, or null if value is null or was not converted successfully.</returns>
        /// <remarks>
        /// If <paramref name="value"/> is a <see cref="string"/>, the conversion behavior is delegated to <see cref="StringExtensions.ToDateTimeOffset(string, DateTimeStyles?, IFormatProvider?)"/>.
        /// If <paramref name="value"/> is a <see cref="DateTime"/>, the offset is determined by its <see cref="DateTime.Kind"/> property:
        /// <list type="bullet">
        /// <item><description><see cref="DateTimeKind.Utc"/> results in a UTC offset (+00:00).</description></item>
        /// <item><description><see cref="DateTimeKind.Local"/> or <see cref="DateTimeKind.Unspecified"/> results in the local system's time zone offset.</description></item>
        /// </list>
        /// For other convertible types, the value is first converted to <see cref="DateTime"/> using <see cref="Convert.ToDateTime(object, IFormatProvider)"/>,
        /// which typically results in an <see cref="DateTimeKind.Unspecified"/> kind, and then converted to <see cref="DateTimeOffset"/> using the local system's time zone offset.
        /// </remarks>
        public static DateTimeOffset? ToDateTimeOffset(this object? value, DateTimeStyles? styles = DateTimeStyles.None, IFormatProvider? provider = null)
        {
            provider ??= CultureInfo.InvariantCulture;
            if (value == null) return null;
            if (value is DateTimeOffset dto) return dto;
            if (value is string s) return s.ToDateTimeOffset(styles, provider);
            if (value is long l) return DateTimeOffset.FromUnixTimeSeconds(l); // Handle Unix seconds
            if (value is double d) return new DateTimeOffset(DateTime.FromOADate(d)); // Handle OLE Automation Date
            try
            {
                if (value is DateTime dt)
                {
                    return new DateTimeOffset(dt);
                }
                else
                {
                    var converted = Convert.ToDateTime(value, provider);
                    return new DateTimeOffset(converted);
                }
            }
            catch (Exception ex) when (ex is FormatException || ex is InvalidCastException || ex is OverflowException || ex is ArgumentOutOfRangeException)
            {
                return null;
            }
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
#if NET5_0_OR_GREATER
        [Obsolete("This method is obsolete. Use ToDateOnly instead for .NET 6 and later.", error: false)]
#endif
        public static DateTime? ToDate(this object? value, DateTimeStyles? styles = DateTimeStyles.None, IFormatProvider? provider = null)
        {
            return value?.ToDateTime(styles, provider)?.Date;
        }

#if NET5_0_OR_GREATER
        /// <summary>
        /// Converts the value of the specified object to an equivalent <see cref="DateOnly"/>.
        /// </summary>
        /// <param name="value">An object that supplies the value to convert, or null.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information. Default is <see cref="CultureInfo.InvariantCulture"/></param>
        /// <returns>A <see cref="DateOnly"/> equivalent of the value, or null if conversion fails.</returns>
        /// <remarks>If the object contains time information, it will be omitted.</remarks>
        public static DateOnly? ToDateOnly(this object? value, IFormatProvider? provider = null)
        {
            provider ??= CultureInfo.InvariantCulture;
            if (value == null) return null;
            if (value is DateOnly dateOnlyValue) return dateOnlyValue;
            if (value is string s) return s.ToDateOnly(provider);
            try
            {
                if (value is DateTime dt)
                {
                    return DateOnly.FromDateTime(dt);
                }
                else if (value is DateTimeOffset dto)
                {
                    return DateOnly.FromDateTime(dto.Date);
                }
                else
                {
                    var converted = Convert.ToDateTime(value, provider);
                    return DateOnly.FromDateTime(converted);
                }
            }
            catch (Exception ex) when (ex is FormatException || ex is InvalidCastException || ex is OverflowException)
            {
                return null;
            }
        }
#endif

        /// <summary>
        /// Converts the value of the specified object to an equivalent enumeration type.
        /// </summary>
        /// <typeparam name="T">An enumeration type.</typeparam>
        /// <param name="value">An object that supplies the value to convert, or null.</param>
        /// <returns>An object of type T whose value is represented by value, or null if conversion fails.</returns>
        /// <remarks>The result is not validated against a defined member.</remarks>
        public static T? ToEnum<T>(this object? value) where T : struct, Enum
        {
            if (value is T v)
            {
                return v;
            }

            var text = value?.ToString();
            return text.ToEnum<T>();
        }
    }
}

