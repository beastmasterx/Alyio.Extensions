// MIT License

using Xunit;

namespace Alyio.Extensions.Tests
{
    public class ObjectExtensionsTests
    {
        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData("true", true)]
        [InlineData("false", false)]
        [InlineData("1", true)]
        [InlineData("0", false)]
        [InlineData(null, false)]
        public void ToBoolean_Should_Convert_Correctly(object? input, bool expected)
        {
            Assert.Equal(expected, input.ToBoolean(CultureInfo.InvariantCulture));
        }

        [Theory]
        [InlineData(9527, 9527)]
        [InlineData("9527", 9527)]
        [InlineData(9527.0, 9527)]
        [InlineData(null, null)]
        [InlineData("x", null)]
        public void ToInt32_Should_Convert_Correctly(object? input, int? expected)
        {
            Assert.Equal(expected, input.ToInt32(CultureInfo.InvariantCulture));
        }

        [Theory]
        [InlineData(9527L, 9527L)]
        [InlineData("9527", 9527L)]
        [InlineData(9527.0, 9527L)]
        [InlineData(null, null)]
        [InlineData("x", null)]
        public void ToInt64_Should_Convert_Correctly(object? input, long? expected)
        {
            Assert.Equal(expected, input.ToInt64(CultureInfo.InvariantCulture));
        }

        [Theory]
        [InlineData(9527.5, 9527.5)]
        [InlineData("9527.5", 9527.5)]
        [InlineData(9527, 9527.0)]
        [InlineData(null, null)]
        [InlineData("x", null)]
        public void ToDouble_Should_Convert_Correctly(object? input, double? expected)
        {
            Assert.Equal(expected, input.ToDouble(CultureInfo.InvariantCulture));
        }

        [Theory]
        [MemberData(nameof(TestData.ObjectToDecimal), MemberType = typeof(TestData))]
        public void ToDecimal_Should_Convert_Correctly(object? input, decimal? expected)
        {
            var decimalExpected = expected.HasValue ? expected.Value : (decimal?)null;
            var inputAsDecimal = input is double d ? (decimal)d : input;
            Assert.Equal(decimalExpected, inputAsDecimal.ToDecimal(CultureInfo.InvariantCulture));
        }

        [Fact]
        public void ToDateTime_Should_Convert_Correctly()
        {
            var now = new DateTime(2021, 12, 13, 14, 15, 16);
            var nowString = now.ToString(CultureInfo.InvariantCulture);

            Assert.Equal(now, now.ToDateTime(null, CultureInfo.InvariantCulture));
            Assert.Equal(now, nowString.ToDateTime(null, CultureInfo.InvariantCulture));
            Assert.Null(((object?)null).ToDateTime(null, CultureInfo.InvariantCulture));
            Assert.Null("x".ToDateTime(null, CultureInfo.InvariantCulture));
        }

        [Fact]
        public void ToDate_Should_Return_Date_Part_Only()
        {
            var now = DateTime.Now;
            Assert.Equal(now.Date, now.ToDate(null, CultureInfo.InvariantCulture));
            Assert.Null(((object?)null).ToDate(null, CultureInfo.InvariantCulture));
        }

        [Theory]
        [InlineData(FileMode.Open, FileMode.Open)]
        [InlineData("Open", FileMode.Open)]
        [InlineData("open", FileMode.Open)]
        [InlineData(3, FileMode.Open)]
        [InlineData("3", FileMode.Open)]
        [InlineData("invalid", null)]
        [InlineData(null, null)]
        public void ToEnum_Should_Convert_Correctly(object? input, FileMode? expected)
        {
            Assert.Equal(expected, input.ToEnum<FileMode>());
        }

        [Theory]
        [InlineData("hello", "hello")]
        [InlineData(123, "123")]
        [InlineData(null, "")]
        public void ToStringOrEmpty_Should_Return_Correct_String(object? input, string expected)
        {
            Assert.Equal(expected, input.ToStringOrEmpty());
        }
    }
}
