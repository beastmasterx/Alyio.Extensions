// MIT License

using Xunit;

namespace Alyio.Extensions.Tests
{
    public class ByteExtensionsTests
    {
        [Theory]
        [InlineData(new byte[] { 0x19, 0xE9, 0xB9, 0xF3, 0x35, 0x0B, 0x49, 0x18, 0x9A, 0x2C, 0xC2, 0x7D, 0x66, 0x75, 0x41, 0xC5 }, "19E9B9F3350B49189A2CC27D667541C5")]
        [InlineData(new byte[] { 0x01, 0x02, 0x03 }, "010203")]
        [InlineData(new byte[] { 0xFF, 0xEE, 0xDD }, "FFEEDD")]
        [InlineData(new byte[] { }, "")]
        public void ToHex_With_Valid_ByteArray_Should_Return_Correct_HexString(byte[] bytes, string expectedHex)
        {
            var hex = bytes.ToHex();
            Assert.Equal(expectedHex, hex);
        }

        [Fact]
        public void ToHex_With_Null_ByteArray_Should_Return_Null()
        {
            byte[]? bytes = null;
            var hex = bytes.ToHex();
            Assert.Null(hex);
        }
    }
}
