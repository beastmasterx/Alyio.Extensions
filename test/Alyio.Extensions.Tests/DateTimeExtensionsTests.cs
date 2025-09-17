// MIT License

using Xunit;

namespace Alyio.Extensions.Tests
{
    public class DateTimeExtensionsTests
    {
        [Theory]
        [InlineData(2025, 9, 15, 20250915)]
        [InlineData(2024, 2, 29, 20240229)]
        [InlineData(1999, 12, 31, 19991231)]
        public void ToDateInt32_Should_Return_Correct_Integer(int year, int month, int day, int expected)
        {
            var dt = new DateTime(year, month, day);
            Assert.Equal(expected, dt.ToDateInt32());
        }

        [Fact]
        public void ToyyyyMMddHHmmss_Should_Return_Correctly_Formatted_String()
        {
            var dt = new DateTime(2025, 9, 15, 14, 30, 55);
            var expected = "2025-09-15 14:30:55";
            Assert.Equal(expected, dt.ToyyyyMMddHHmmss());
        }

        [Theory]
        [InlineData(1497002400L, 2017, 6, 9, 10, 0, 0)] // Original test case
        [InlineData(0L, 1970, 1, 1, 0, 0, 0)]           // Unix epoch
        [InlineData(1663200000L, 2022, 9, 15, 0, 0, 0)]
        public void ToUnix_And_ToDateTime_FromLong_Should_Convert_Correctly(long unixTimestamp, int year, int month, int day, int hour, int min, int sec)
        {
            // Test long -> DateTime
            var expectedDateTime = new DateTime(year, month, day, hour, min, sec, DateTimeKind.Utc);
            var actualDateTime = unixTimestamp.ToDateTime();
            Assert.NotNull(actualDateTime);
            Assert.Equal(expectedDateTime, actualDateTime.Value.ToUniversalTime());

            // Test DateTime -> long
            var actualUnixTimestamp = actualDateTime.Value.ToUnix();
            Assert.Equal(unixTimestamp, actualUnixTimestamp);
        }

        [Theory]
        [InlineData(1497002400.5, 2017, 6, 9, 10, 0, 0, 500)] // With milliseconds
        [InlineData(0.0, 1970, 1, 1, 0, 0, 0, 0)]
        public void ToDateTime_FromDouble_Should_Convert_Correctly(double unixTimestamp, int year, int month, int day, int hour, int min, int sec, int ms)
        {
            var expectedDateTime = new DateTime(year, month, day, hour, min, sec, ms, DateTimeKind.Utc);
            var actualDateTime = unixTimestamp.ToDateTime();
            Assert.NotNull(actualDateTime);
            Assert.Equal(expectedDateTime, actualDateTime.Value.ToUniversalTime());
        }

        [Theory]
        [InlineData(-1L)]
        [InlineData(long.MinValue)]
        public void ToDateTime_FromInvalidLong_Should_Return_Null(long invalidTimestamp)
        {
            Assert.Null(invalidTimestamp.ToDateTime());
        }

        [Theory]
        [InlineData(-1.0)]
        [InlineData(double.NaN)]
        [InlineData(double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity)]
        public void ToDateTime_FromInvalidDouble_Should_Return_Null(double invalidTimestamp)
        {
            Assert.Null(invalidTimestamp.ToDateTime());
        }
    }
}
