using System;

namespace Alyio.Extensions
{
    /// <summary>
    /// Extension methods for converting a <see cref="Byte"/> type to another base data type.
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// Converts the numeric value of each element of a specified array of bytes to its equivalent hexadecimal string representation.
        /// </summary>
        /// <param name="bytes">An array of bytes.</param>
        /// <returns>A string of hexadecimal pairs where each pair represents the corresponding element in value; for example, "7F2C4A00". null if the <paramref name="bytes"/> is null.</returns>
        public static string? ToHex(this byte[]? bytes)
        {
            if (bytes == null)
            {
                return null;
            }

            return BitConverter.ToString(bytes).Replace("-", string.Empty);
        }
    }
}
