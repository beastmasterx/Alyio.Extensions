// MIT License

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
            if (bytes.Length == 0)
            {
                return string.Empty;
            }
#if NET5_0_OR_GREATER
            return Convert.ToHexString(bytes);
#else
            // Each byte is represented by two hexadecimal characters.
            var c = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                // Get the high 4 bits of the byte (the first hex character).
                var b = (byte)(bytes[i] >> 4);
                // Convert the 4-bit value to its hex character representation ('0'-'9', 'A'-'F').
                c[i * 2] = (char)(b < 10 ? b + '0' : b + 'A' - 10);

                // Get the low 4 bits of the byte (the second hex character).
                b = (byte)(bytes[i] & 0x0F);
                // Convert the 4-bit value to its hex character representation ('0'-'9', 'A'-'F').
                c[i * 2 + 1] = (char)(b < 10 ? b + '0' : b + 'A' - 10);
            }
            return new string(c);
#endif
        }
    }
}
