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

        #region DateTimeOffset Tests

        [Theory]
        [InlineData(1999, 12, 31, 23, 59, 59, DateTimeKind.Utc)]
        [InlineData(1999, 12, 31, 23, 59, 59, DateTimeKind.Local)]
        [InlineData(1999, 12, 31, 23, 59, 59, DateTimeKind.Unspecified)]
        public void ToDateTimeOffset_FromDateTime_Should_Convert_Correctly(int year, int month, int day, int hour, int min, int sec, DateTimeKind kind)
        {
            var dateTime = new DateTime(year, month, day, hour, min, sec, kind);
            var dateTimeOffset = dateTime.ToDateTimeOffset();

            Assert.Equal(dateTime, dateTimeOffset.LocalDateTime);
            Assert.Equal(TimeZoneInfo.Local.BaseUtcOffset, dateTimeOffset.Offset);
        }

        [Fact]
        public void ToDateTimeOffset_FromDateTime_WithDifferentKinds_Should_Handle_Correctly()
        {
            // Test with UTC DateTime
            var utcDateTime = new DateTime(2025, 9, 15, 14, 30, 55, DateTimeKind.Utc);
            var utcOffset = utcDateTime.ToDateTimeOffset();
            Assert.Equal(utcDateTime, utcOffset.DateTime);
            Assert.Equal(TimeSpan.Zero, utcOffset.Offset);

            // Test with Local DateTime
            var localDateTime = new DateTime(2025, 9, 15, 14, 30, 55, DateTimeKind.Local);
            var localOffset = localDateTime.ToDateTimeOffset();
            Assert.Equal(localDateTime, localOffset.DateTime);
            Assert.Equal(TimeSpan.Zero, localOffset.Offset);

            // Test with Unspecified DateTime
            var unspecifiedDateTime = new DateTime(2025, 9, 15, 14, 30, 55, DateTimeKind.Unspecified);
            var unspecifiedOffset = unspecifiedDateTime.ToDateTimeOffset();
            Assert.Equal(unspecifiedDateTime, unspecifiedOffset.DateTime);
            Assert.Equal(TimeSpan.Zero, unspecifiedOffset.Offset);
        }

        [Theory]
        [InlineData(2025, 9, 15, 14, 30, 55, 0, 0)] // UTC
        [InlineData(2024, 2, 29, 12, 0, 0, 5, 30)]  // +05:30 offset
        [InlineData(1999, 12, 31, 23, 59, 59, -8, 0)] // -08:00 offset
        public void ToDateTime_FromDateTimeOffset_Should_Convert_To_UTC(int year, int month, int day, int hour, int min, int sec, int offsetHours, int offsetMinutes)
        {
            var offset = new TimeSpan(offsetHours, offsetMinutes, 0);
            var dateTimeOffset = new DateTimeOffset(year, month, day, hour, min, sec, offset);
            var utcDateTime = dateTimeOffset.ToDateTime();

            Assert.Equal(dateTimeOffset.UtcDateTime, utcDateTime);
            Assert.Equal(DateTimeKind.Utc, utcDateTime.Kind);
        }

        [Theory]
        [InlineData(1497002400L, 2017, 6, 9, 10, 0, 0)] // Original test case
        [InlineData(0L, 1970, 1, 1, 0, 0, 0)]           // Unix epoch
        [InlineData(1663200000L, 2022, 9, 15, 0, 0, 0)]
        [InlineData(1609459200L, 2021, 1, 1, 0, 0, 0)]  // New Year 2021
        public void ToUnix_FromDateTimeOffset_Should_Convert_Correctly(long expectedUnixTimestamp, int year, int month, int day, int hour, int min, int sec)
        {
            var dateTimeOffset = new DateTimeOffset(year, month, day, hour, min, sec, TimeSpan.Zero);
            var actualUnixTimestamp = dateTimeOffset.ToUnix();

            Assert.Equal(expectedUnixTimestamp, actualUnixTimestamp);
        }

        [Theory]
        [InlineData(1497002400L, 2017, 6, 9, 10, 0, 0)] // Original test case
        [InlineData(0L, 1970, 1, 1, 0, 0, 0)]           // Unix epoch
        [InlineData(1663200000L, 2022, 9, 15, 0, 0, 0)]
        [InlineData(1609459200L, 2021, 1, 1, 0, 0, 0)]  // New Year 2021
        public void ToDateTimeOffset_FromLong_Should_Convert_Correctly(long unixTimestamp, int year, int month, int day, int hour, int min, int sec)
        {
            var dateTimeOffset = unixTimestamp.ToDateTimeOffset();

            Assert.Equal(year, dateTimeOffset.Year);
            Assert.Equal(month, dateTimeOffset.Month);
            Assert.Equal(day, dateTimeOffset.Day);
            Assert.Equal(hour, dateTimeOffset.Hour);
            Assert.Equal(min, dateTimeOffset.Minute);
            Assert.Equal(sec, dateTimeOffset.Second);
            Assert.Equal(TimeSpan.Zero, dateTimeOffset.Offset);
        }

        [Fact]
        public void ToDateTimeOffset_FromLong_Should_Handle_Edge_Cases()
        {
            // Test with zero timestamp (Unix epoch)
            var epochOffset = 0L.ToDateTimeOffset();
            Assert.Equal(1970, epochOffset.Year);
            Assert.Equal(1, epochOffset.Month);
            Assert.Equal(1, epochOffset.Day);
            Assert.Equal(0, epochOffset.Hour);
            Assert.Equal(0, epochOffset.Minute);
            Assert.Equal(0, epochOffset.Second);
            Assert.Equal(TimeSpan.Zero, epochOffset.Offset);

            // Test with large timestamp
            var largeTimestamp = 4102444800L; // 2100-01-01 00:00:00 UTC
            var largeOffset = largeTimestamp.ToDateTimeOffset();
            Assert.Equal(2100, largeOffset.Year);
            Assert.Equal(1, largeOffset.Month);
            Assert.Equal(1, largeOffset.Day);
            Assert.Equal(TimeSpan.Zero, largeOffset.Offset);
        }

        [Fact]
        public void DateTimeOffset_RoundTrip_Conversion_Should_Work()
        {
            // Test DateTime -> DateTimeOffset -> DateTime
            var originalDateTime = new DateTime(2025, 9, 15, 14, 30, 55, DateTimeKind.Utc);
            var dateTimeOffset = originalDateTime.ToDateTimeOffset();
            var convertedDateTime = dateTimeOffset.ToDateTime();

            Assert.Equal(originalDateTime.ToUniversalTime(), convertedDateTime.ToUniversalTime());

            // Test long -> DateTimeOffset -> long
            var originalUnix = 1497002400L;
            var dateTimeOffsetFromUnix = originalUnix.ToDateTimeOffset();
            var convertedUnix = dateTimeOffsetFromUnix.ToUnix();

            Assert.Equal(originalUnix, convertedUnix);
        }

        [Fact]
        public void DateTimeOffset_WithDifferentOffsets_Should_Convert_To_Same_UTC()
        {
            var baseDateTime = new DateTime(2025, 9, 15, 14, 30, 55, DateTimeKind.Unspecified);
            var baseUtcDateTime = DateTime.SpecifyKind(baseDateTime, DateTimeKind.Utc);

            // Create DateTimeOffset instances with different offsets but same UTC time
            var utcOffset = new DateTimeOffset(baseDateTime, TimeSpan.Zero);
            var plus5Offset = new DateTimeOffset(baseDateTime.AddHours(-5), TimeSpan.FromHours(-5));
            var minus8Offset = new DateTimeOffset(baseDateTime.AddHours(8), TimeSpan.FromHours(8));

            // All should convert to the same UTC DateTime
            var utcDateTime1 = utcOffset.ToDateTime();
            var utcDateTime2 = plus5Offset.ToDateTime();
            var utcDateTime3 = minus8Offset.ToDateTime();

            Assert.Equal(baseUtcDateTime, utcDateTime1);
            Assert.Equal(baseUtcDateTime, utcDateTime2);
            Assert.Equal(baseUtcDateTime, utcDateTime3);
        }

        #endregion
    }
}
