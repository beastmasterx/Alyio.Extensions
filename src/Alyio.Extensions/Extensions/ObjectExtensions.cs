using System;
using System.Globalization;

namespace Alyio.Extensions
{
    /// <summary>
    /// Extension methods for converting a <see cref="object"/> type to another base data type.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Converts the value of a specified object to an equivalent <see cref="Boolean"/> value.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <param name="provider">An <see cref="System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information. Default is <see cref="CultureInfo.InvariantCulture"/></param>
        /// <returns>
        /// <see cref="System.Object"/>: true or false, which reflects the value returned by invoking the <see cref="IConvertible.ToBoolean"/> method for the underlying type of value. If value is null, the method returns false. 
        /// <see cref="System.String"/>: true if value equals <see cref="Boolean.TrueString"/>, or false if value equals <see cref="Boolean.FalseString"/> or null.
        /// <see cref="System.Double"/>: true if value is not zero; otherwise, false.
        /// </returns>
        public static bool ToBoolean(this object? value, IFormatProvider? provider = null)
        {
            if (value == null)
            {
                return false;
            }

            if (typeof(bool).Equals(value.GetType()))
            {
                return (bool)value;
            }

            if (value is IConvertible converter)
            {
                try
                {
                    return converter.ToBoolean(provider ?? CultureInfo.InvariantCulture);
                }
                catch
                {
                    var d = value.ToDouble(provider ?? CultureInfo.InvariantCulture);
                    if (d != null)
                    {
                        return d != 0D;
                    }
                    else
                    {
                        var s = value.ToString();
                        return s.ToBoolean();
                    }
                }
            }
            else
            {
                var d = value.ToDouble(provider ?? CultureInfo.InvariantCulture);
                if (d != null)
                {
                    return d != 0D;
                }
                else
                {
                    var s = value.ToString();
                    return s.ToBoolean();
                }
            }
        }

        /// <summary>
        /// Converts the value of the specified object to its equivalent string representation.
        /// </summary>
        /// <param name="value">An object that supplies the value to convert, or null.</param>
        /// <returns>The string representation of value, or System.String.Empty if value is null.</returns>
        public static string ToStringExt(this object? value) => value?.ToString() ?? string.Empty;

        /// <summary>
        /// Converts the value of the specified object to a 32-bit signed integer.
        /// </summary>
        /// <param name="value">An object that supplies the value to convert, or null.</param>
        /// <param name="provider">An <see cref="System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information. Default is <see cref="CultureInfo.InvariantCulture"/></param>
        /// <returns>A 32-bit signed integer equivalent to value, or null if value is null or was not converted successfully.</returns>
        public static int? ToInt32(this object? value, IFormatProvider? provider = null)
        {
            if (value == null)
            {
                return null;
            }

            if (typeof(int).Equals(value.GetType()))
            {
                return (int)value;
            }

            if (value is IConvertible converter)
            {
                try
                {
                    return converter.ToInt32(provider ?? CultureInfo.InvariantCulture);
                }
                catch
                {
                    var s = value.ToString();
                    return s.ToInt32();
                }
            }
            else
            {
                var s = value.ToString();
                return s.ToInt32();
            }
        }

        /// <summary>
        /// Converts the value of the specified object to a 64-bit signed integer.
        /// </summary>
        /// <param name="value">An object that supplies the value to convert, or null.</param>
        /// <param name="provider">An <see cref="System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information. Default is <see cref="CultureInfo.InvariantCulture"/></param>
        /// <returns>A 64-bit signed integer equivalent to value, or null if value is null or was not converted successfully.</returns>
        public static long? ToInt64(this object? value, IFormatProvider? provider = null)
        {
            if (value == null)
            {
                return null;
            }

            if (typeof(long).Equals(value.GetType()))
            {
                return (long)value;
            }

            if (value is IConvertible converter)
            {
                try
                {
                    return converter.ToInt64(provider ?? CultureInfo.InvariantCulture);
                }
                catch
                {
                    var s = value.ToString();
                    return s.ToInt64();
                }
            }
            else
            {
                var s = value.ToString();
                return s.ToInt64();
            }
        }

        /// <summary>
        /// Converts the value of the specified object to a double-precision floating-point number.
        /// </summary>
        /// <param name="value">An object that supplies the value to convert, or null.</param>
        /// <param name="provider">An <see cref="System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information. Default is <see cref="CultureInfo.InvariantCulture"/></param>
        /// <returns>A double-precision floating-point number equivalent to value, or null if value is null or was not converted successfully.</returns>
        public static double? ToDouble(this object? value, IFormatProvider? provider = null)
        {
            if (value == null)
            {
                return null;
            }

            if (typeof(double).Equals(value.GetType()))
            {
                return (double)value;
            }

            if (value is IConvertible converter)
            {
                try
                {
                    return converter.ToDouble(provider ?? CultureInfo.InvariantCulture);
                }
                catch
                {
                    var s = value.ToString();
                    return s.ToDouble();
                }
            }
            else
            {
                var s = value.ToString();
                return s.ToDouble();
            }
        }

        /// <summary>
        /// Converts the value of the specified object to a decimal number.
        /// </summary>
        /// <param name="value">An object that supplies the value to convert, or null.</param>
        /// <param name="provider">An <see cref="System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information. Default is <see cref="CultureInfo.InvariantCulture"/></param>
        /// <returns>A decimal number equivalent to value, or null if value is null or was not converted successfully.</returns>
        public static decimal? ToDecimal(this object? value, IFormatProvider? provider = null)
        {
            if (value == null)
            {
                return null;
            }

            if (typeof(decimal).Equals(value.GetType()))
            {
                return (decimal)value;
            }

            if (value is IConvertible converter)
            {
                try
                {
                    return converter.ToDecimal(provider ?? CultureInfo.InvariantCulture);
                }
                catch
                {
                    var s = value.ToString();
                    return s.ToDecimal();
                }
            }
            else
            {
                var s = value.ToString();
                return s.ToDecimal();
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
        /// <param name="provider">An <see cref="System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information. Default is <see cref="CultureInfo.InvariantCulture"/></param>
        /// <returns>The date and time equivalent of the value of value, or null if value is null or was not converted successfully.</returns>
        public static DateTime? ToDateTime(this object? value, DateTimeStyles? styles = DateTimeStyles.None, IFormatProvider? provider = null)
        {
            if (value == null)
            {
                return null;
            }

            if (typeof(DateTime).Equals(value.GetType()))
            {
                return (DateTime)value;
            }

            if (value is IConvertible converter)
            {
                try
                {
                    return converter.ToDateTime(provider ?? CultureInfo.InvariantCulture);
                }
                catch
                {
                    var str = value.ToString();
                    return str.ToDateTime(styles, provider ?? CultureInfo.InvariantCulture);
                }
            }
            else
            {
                var str = value.ToString();
                return str.ToDateTime(styles, provider);
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
        /// <param name="provider">An <see cref="System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information. Default is <see cref="CultureInfo.InvariantCulture"/></param>
        /// <returns>The date without time equivalent of the value of value, or null if value is null or was not converted successfully.</returns>
        public static DateTime? ToDate(this object? value, DateTimeStyles? styles = DateTimeStyles.None, IFormatProvider? provider = null)
        {
            return value?.ToDateTime(styles, provider)?.Date;
        }

        /// <summary>
        /// Converts the value of the specified object to an enumeration type..
        /// </summary>
        /// <typeparam name="T">An enumeration type.</typeparam>
        /// <param name="value">An object that supplies the value to convert, or null.</param>
        /// <returns>An object of type enumType whose value is represented by value.</returns>
        public static T? ToEnum<T>(this object? value) where T : struct, Enum
        {
            if (value is T v)
            {
                return v;
            }

            var text = value?.ToString();
            return Enum.TryParse(text, true, out T e) ? e : default(T);
        }
    }
}
