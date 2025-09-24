// MIT License

using Xunit;

namespace Alyio.Extensions.Tests
{
    public class DateTimeExtensionsTests
    {
        [Fact]
        public void ToDateInt32_FromDateTime_ReturnsCorrectInteger()
        {
            var dt = new DateTime(2006, 1, 2);
            var expected = 20060102;
            Assert.Equal(expected, dt.ToDateInt32());
        }

        [Fact]
        public void ToyyyyMMddHHmmss_FromDateTime_ReturnsCorrectlyFormattedString()
        {
            var dt = new DateTime(2006, 1, 2, 15, 4, 5);
            var expected = "2006-01-02 15:04:05";
            Assert.Equal(expected, dt.ToyyyyMMddHHmmss());
        }

        [Fact]
        public void ToUnix_And_ToDateTime_FromLong_RoundTripConversion_IsCorrect()
        {
            long timestamp = 1136214245L;
            var expectedDateTime = new DateTime(2006, 1, 2, 15, 4, 5, DateTimeKind.Utc);

            // Test long -> DateTime
            var actualDateTime = timestamp.ToDateTime();
            Assert.NotNull(actualDateTime);
            Assert.Equal(expectedDateTime, actualDateTime);

            // Test DateTime -> long
            var actualTimestamp = actualDateTime.Value.ToUnix();
            Assert.Equal(timestamp, actualTimestamp);
        }

        [Fact]
        public void ToDateTime_FromDouble_ReturnsCorrectDateTime()
        {
            double timestamp = 1136214245.999;
            var expectedDateTime = new DateTime(2006, 1, 2, 15, 4, 5, 999, DateTimeKind.Utc);
            var actualDateTime = timestamp.ToDateTime();
            Assert.NotNull(actualDateTime);
            Assert.Equal(expectedDateTime, actualDateTime);
        }

        [Theory]
        [InlineData(-1L)]
        [InlineData(long.MinValue)]
        public void ToDateTime_FromInvalidLong_ReturnsNull(long invalidTimestamp)
        {
            Assert.Null(invalidTimestamp.ToDateTime());
        }

        [Theory]
        [InlineData(-1.0)]
        [InlineData(double.NaN)]
        [InlineData(double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity)]
        public void ToDateTime_FromInvalidDouble_ReturnsNull(double invalidTimestamp)
        {
            Assert.Null(invalidTimestamp.ToDateTime());
        }

        #region DateTimeOffset Tests

        [Fact]
        public void ToDateTimeOffset_FromLocalDateTime_ReturnsCorrectDateTimeOffset()
        {
            var dateTime = new DateTime(2006, 1, 2, 15, 4, 5, DateTimeKind.Local);
            var dateTimeOffset = dateTime.ToDateTimeOffset();

            Assert.Equal(dateTime, dateTimeOffset.LocalDateTime);
            Assert.Equal(TimeZoneInfo.Local.BaseUtcOffset, dateTimeOffset.Offset);
        }

        [Fact]
        public void ToDateTimeOffset_FromUnspecifiedDateTime_ReturnsCorrectDateTimeOffset()
        {
            var dateTime = new DateTime(2006, 1, 2, 15, 4, 5, DateTimeKind.Unspecified);
            var dateTimeOffset = dateTime.ToDateTimeOffset();

            Assert.Equal(dateTime, dateTimeOffset.LocalDateTime);
            Assert.Equal(TimeZoneInfo.Local.BaseUtcOffset, dateTimeOffset.Offset);
        }

        [Fact]
        public void ToDateTimeOffset_FromUtcDateTime_HandlesCorrectly()
        {
            var utcDateTime = new DateTime(2006, 1, 2, 15, 4, 5, DateTimeKind.Utc);
            var utcOffset = utcDateTime.ToDateTimeOffset();
            Assert.Equal(utcDateTime, utcOffset.DateTime);
            Assert.Equal(TimeSpan.Zero, utcOffset.Offset);
        }

        [Theory]
        [InlineData(2006, 1, 2, 15, 4, 5, 0, 0)] // UTC
        [InlineData(2006, 1, 2, 15, 4, 5, 5, 30)]  // +05:30 offset
        [InlineData(2006, 1, 2, 15, 4, 5, -8, 0)] // -08:00 offset
        public void ToDateTime_FromDateTimeOffset_ConvertsToUtc(int year, int month, int day, int hour, int min, int sec, int offsetHours, int offsetMinutes)
        {
            var offset = new TimeSpan(offsetHours, offsetMinutes, 0);
            var dateTimeOffset = new DateTimeOffset(year, month, day, hour, min, sec, offset);
            var utcDateTime = dateTimeOffset.ToDateTime();

            Assert.Equal(dateTimeOffset.UtcDateTime, utcDateTime);
            Assert.Equal(DateTimeKind.Utc, utcDateTime.Kind);
        }

        [Fact]
        public void ToUnix_FromDateTimeOffset_ReturnsCorrectUnixTimestamp()
        {
            long expectedTimestamp = 1136214245L;
            var dateTimeOffset = new DateTimeOffset(2006, 1, 2, 15, 4, 5, TimeSpan.Zero);
            var actualTimestamp = dateTimeOffset.ToUnix();

            Assert.Equal(expectedTimestamp, actualTimestamp);
        }

        [Fact]
        public void ToDateTimeOffset_FromLong_ReturnsCorrectDateTimeOffset()
        {
            long timestamp = 1136214245L;
            var dateTimeOffset = timestamp.ToDateTimeOffset();

            Assert.Equal(2006, dateTimeOffset.Year);
            Assert.Equal(1, dateTimeOffset.Month);
            Assert.Equal(2, dateTimeOffset.Day);
            Assert.Equal(15, dateTimeOffset.Hour);
            Assert.Equal(4, dateTimeOffset.Minute);
            Assert.Equal(5, dateTimeOffset.Second);
            Assert.Equal(TimeSpan.Zero, dateTimeOffset.Offset);
        }

        [Fact]
        public void ToDateTimeOffset_FromLong_WithEdgeCases_HandlesCorrectly()
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
            var largeTimestamp = 1136214245L; // 2006-01-02 15:04:05 UTC
            var largeOffset = largeTimestamp.ToDateTimeOffset();
            Assert.Equal(2006, largeOffset.Year);
            Assert.Equal(1, largeOffset.Month);
            Assert.Equal(2, largeOffset.Day);
            Assert.Equal(TimeSpan.Zero, largeOffset.Offset);
        }

        [Fact]
        public void DateTimeOffset_RoundTripConversion_WorksCorrectly()
        {
            // Test DateTime -> DateTimeOffset -> DateTime
            var originalDateTime = new DateTime(2006, 1, 2, 15, 4, 5, DateTimeKind.Utc);
            var dateTimeOffset = originalDateTime.ToDateTimeOffset();
            var convertedDateTime = dateTimeOffset.ToDateTime();

            Assert.Equal(originalDateTime.ToUniversalTime(), convertedDateTime.ToUniversalTime());

            // Test long -> DateTimeOffset -> long
            var originalUnix = 1136214245L;
            var dateTimeOffsetFromUnix = originalUnix.ToDateTimeOffset();
            var convertedUnix = dateTimeOffsetFromUnix.ToUnix();

            Assert.Equal(originalUnix, convertedUnix);
        }

        [Fact]
        public void ToDateTime_FromDateTimeOffset_WithDifferentOffsets_ConvertsToSameUtc()
        {
            var baseDateTime = new DateTime(2006, 1, 2, 15, 4, 5, DateTimeKind.Unspecified);
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
