// MIT License

using Xunit;

namespace Alyio.Extensions.Tests
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("true", true)]
        [InlineData("True", true)]
        [InlineData("false", false)]
        [InlineData("False", false)]
        [InlineData(" ", false)]
        [InlineData(null, false)]
        [InlineData("1", true)] // This is a change in behavior from the old implementation, but aligns with Convert.ToBoolean
        [InlineData("0", false)]
        public void ToBoolean_Should_Convert_Correctly(string? input, bool expected)
        {
            Assert.Equal(expected, input.ToBoolean());
        }

        [Theory]
        [InlineData("9527", 9527)]
        [InlineData("-100", -100)]
        [InlineData("0", 0)]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData(" ", null)]
        [InlineData("x", null)]
        [InlineData("2147483648", null)] // int.MaxValue + 1
        public void ToInt32_Should_Convert_Correctly(string? input, int? expected)
        {
            Assert.Equal(expected, input.ToInt32(CultureInfo.InvariantCulture));
        }

        [Theory]
        [InlineData("9527", 9527L)]
        [InlineData("-100", -100L)]
        [InlineData("0", 0L)]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData(" ", null)]
        [InlineData("x", null)]
        [InlineData("9223372036854775808", null)] // long.MaxValue + 1
        public void ToInt64_Should_Convert_Correctly(string? input, long? expected)
        {
            Assert.Equal(expected, input.ToInt64(CultureInfo.InvariantCulture));
        }

        [Theory]
        [InlineData("9527.5", 9527.5)]
        [InlineData("-100.5", -100.5)]
        [InlineData("0.0", 0.0)]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData(" ", null)]
        [InlineData("x", null)]
        public void ToDouble_Should_Convert_Correctly(string? input, double? expected)
        {
            Assert.Equal(expected, input.ToDouble(CultureInfo.InvariantCulture));
        }

        [Theory]
        [MemberData(nameof(TestData.StringToDecimal), MemberType = typeof(TestData))]
        public void ToDecimal_Should_Convert_Correctly(string? input, decimal? expected)
        {
            var decimalExpected = expected ?? null;
            var result = input.ToDecimal(CultureInfo.InvariantCulture);
            Assert.Equal(decimalExpected, result);
        }

        [Fact]
        public void ToDateTime_Should_Convert_Correctly()
        {
            var now = new DateTime(2021, 12, 13, 14, 15, 16);
            var nowString = now.ToString(CultureInfo.InvariantCulture);

            Assert.Equal(now, nowString.ToDateTime(null, CultureInfo.InvariantCulture));
            Assert.Null(((string?)null).ToDateTime(null, CultureInfo.InvariantCulture));
            Assert.Null("".ToDateTime(null, CultureInfo.InvariantCulture));
            Assert.Null(" ".ToDateTime(null, CultureInfo.InvariantCulture));
            Assert.Null("x".ToDateTime(null, CultureInfo.InvariantCulture));
        }
    }
}
