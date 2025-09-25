// MIT License

using System.Globalization;
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
        public void ToDateTimeOffset_Should_Convert_Correctly()
        {
            // 1. Null input
            Assert.Null(((object?)null).ToDateTimeOffset(styles: DateTimeStyles.None, provider: CultureInfo.InvariantCulture));

            // 2. DateTimeOffset input
            var dto = new DateTimeOffset(2006, 1, 2, 15, 4, 5, TimeSpan.FromHours(2));
            Assert.Equal(dto, dto.ToDateTimeOffset(styles: DateTimeStyles.None, provider: CultureInfo.InvariantCulture));

            // 3. String input (delegates to StringExtensions)
            var dateStringRFC3339 = "2006-01-02T15:04:05+02:00"; // Corrected RFC3339 format
            Assert.Equal(dto, dateStringRFC3339.ToDateTimeOffset(styles: DateTimeStyles.None, provider: CultureInfo.InvariantCulture));

            var dateStringDateTime = "2006-01-02 15:04:05";
            var expectedFromDateTimeString = new DateTimeOffset(new DateTime(2006, 1, 2, 15, 4, 5), TimeZoneInfo.Local.GetUtcOffset(new DateTime(2006, 1, 2, 15, 4, 5)));
            Assert.Equal(expectedFromDateTimeString, dateStringDateTime.ToDateTimeOffset(styles: DateTimeStyles.None, provider: CultureInfo.InvariantCulture));

            // 4. DateTime input
            // Utc
            var dtUtc = new DateTime(2006, 1, 2, 15, 4, 5, DateTimeKind.Utc);
            Assert.Equal(new DateTimeOffset(dtUtc), dtUtc.ToDateTimeOffset(styles: DateTimeStyles.None, provider: CultureInfo.InvariantCulture));

            // Local
            var dtLocal = new DateTime(2006, 1, 2, 15, 4, 5, DateTimeKind.Local);
            Assert.Equal(new DateTimeOffset(dtLocal), dtLocal.ToDateTimeOffset(styles: DateTimeStyles.None, provider: CultureInfo.InvariantCulture));

            // Unspecified (behaves like Local when converted to DateTimeOffset)
            var dtUnspecified = new DateTime(2006, 1, 2, 15, 4, 5, DateTimeKind.Unspecified);
            Assert.Equal(new DateTimeOffset(dtUnspecified), dtUnspecified.ToDateTimeOffset(styles: DateTimeStyles.None, provider: CultureInfo.InvariantCulture));

            // 5. Other convertible types (e.g., long, double)
            // Unix timestamp (seconds)
            var unixSeconds = 1136214245L; // 2006-01-02 15:04:05 UTC
            var expectedDtoFromLong = DateTimeOffset.FromUnixTimeSeconds(unixSeconds);
            Assert.Equal(expectedDtoFromLong, unixSeconds.ToDateTimeOffset(styles: DateTimeStyles.None, provider: CultureInfo.InvariantCulture));

            // Double (OLE Automation Date) - not directly supported by Convert.ToDateTime for DateTimeOffset
            // Convert.ToDateTime(double) treats it as OLE Automation Date.
            // 38719.627835648148 is 2006-01-02 15:04:05
            var oleAutomationDate = 38719.627835648148;
            var expectedDtoFromDouble = new DateTimeOffset(DateTime.FromOADate(oleAutomationDate));
            Assert.Equal(expectedDtoFromDouble, oleAutomationDate.ToDateTimeOffset(styles: DateTimeStyles.None, provider: CultureInfo.InvariantCulture));

            // 6. Invalid input
            Assert.Null("invalid date".ToDateTimeOffset(styles: DateTimeStyles.None, provider: CultureInfo.InvariantCulture));
            Assert.Null(new object().ToDateTimeOffset(styles: DateTimeStyles.None, provider: CultureInfo.InvariantCulture)); // Arbitrary object
        }

        [Fact]
        public void ToDateTimeOffset_With_Styles_And_Provider_Should_Convert_Correctly()
        {
            var provider = CultureInfo.GetCultureInfo("en-US");

            // String with specific format and styles
            var dateString = "01/02/2006 03:04:05 PM +02:00";
            var expectedDto = new DateTimeOffset(2006, 1, 2, 15, 4, 5, TimeSpan.FromHours(2));
            Assert.Equal(expectedDto, dateString.ToDateTimeOffset(DateTimeStyles.None, provider));

            // String without offset, assume local
            var dateStringNoOffset = "01/02/2006 03:04:05 PM";
            var expectedDtoLocal = new DateTimeOffset(new DateTime(2006, 1, 2, 15, 4, 5), TimeZoneInfo.Local.GetUtcOffset(new DateTime(2006, 1, 2, 15, 4, 5)));
            Assert.Equal(expectedDtoLocal, dateStringNoOffset.ToDateTimeOffset(DateTimeStyles.None, provider));

            // String without offset, assume UTC
            var dateStringAssumeUtc = "01/02/2006 03:04:05 PM";
            var expectedDtoUtc = new DateTimeOffset(2006, 1, 2, 15, 4, 5, TimeSpan.Zero);
            Assert.Equal(expectedDtoUtc, dateStringAssumeUtc.ToDateTimeOffset(DateTimeStyles.AssumeUniversal, provider));
        }



        [Theory]
        [InlineData("hello", "hello")]
        [InlineData(123, "123")]
        [InlineData(null, "")]
        public void ToStringOrEmpty_Should_Return_Correct_String(object? input, string expected)
        {
            Assert.Equal(expected, input.ToStringOrEmpty());
        }

#if NET5_0_OR_GREATER
        [Fact]
        public void ToDateOnly_Object_DateOnly_Should_Return_CorrectDateOnly()
        {
            var dateOnly = new DateOnly(2006, 1, 2);
            Assert.Equal(dateOnly, dateOnly.ToDateOnly(CultureInfo.InvariantCulture));
        }

        [Fact]
        public void ToDateOnly_Object_DateTime_Should_Return_CorrectDateOnly()
        {
            var dateTime = new DateTime(2006, 1, 2, 15, 4, 5);
            var expected = new DateOnly(2006, 1, 2);
            Assert.Equal(expected, dateTime.ToDateOnly(CultureInfo.InvariantCulture));
        }

        [Fact]
        public void ToDateOnly_Object_DateTimeOffset_Should_Return_CorrectDateOnly()
        {
            var dateTimeOffset = new DateTimeOffset(2006, 1, 2, 15, 4, 5, TimeSpan.FromHours(2));
            var expected = new DateOnly(2006, 1, 2);
            Assert.Equal(expected, dateTimeOffset.ToDateOnly(CultureInfo.InvariantCulture));
        }

        [Theory]
        [InlineData("2006-01-02", 2006, 1, 2)]
        [InlineData("01/02/2006", 2006, 1, 2)]
        [InlineData("2006-01-02 15:04:05", 2006, 1, 2)] // Time should be omitted
        public void ToDateOnly_Object_String_Should_Delegate_To_String_Method(string input, int year, int month, int day)
        {
            var expected = new DateOnly(year, month, day);
            Assert.Equal(expected, input.ToDateOnly(CultureInfo.InvariantCulture));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(123)] // Integer
        [InlineData(123.45)] // Double
        [InlineData(FileMode.Open)] // Enum
        [InlineData("invalid date")]
        public void ToDateOnly_Object_Invalid_Values_Should_Return_Null(object? input)
        {
            Assert.Null(input.ToDateOnly(CultureInfo.InvariantCulture));
        }
#endif

        #region ToEnum Tests

        [Theory]
        [InlineData(FileAccess.Read, FileAccess.Read)]
        [InlineData(FileAccess.Write, FileAccess.Write)]
        [InlineData(FileAccess.ReadWrite, FileAccess.ReadWrite)]
        public void ToEnum_Object_FileAccess_To_Same_Enum_Should_Convert_Correctly(FileAccess input, FileAccess expected)
        {
            Assert.Equal(expected, input.ToEnum<FileAccess>());
        }

        [Theory]
        [InlineData(1, FileMode.CreateNew)]
        [InlineData(2, FileMode.Create)]
        [InlineData(3, FileMode.Open)]
        [InlineData(4, FileMode.OpenOrCreate)]
        [InlineData(5, FileMode.Truncate)]
        [InlineData(6, FileMode.Append)]
        public void ToEnum_Object_Integer_To_Enum_Should_Convert_Correctly(int input, FileMode expected)
        {
            Assert.Equal(expected, input.ToEnum<FileMode>());
        }

        [Fact]
        public void ToEnum_Object_Null_Should_Return_Null()
        {
            Assert.Null(((object?)null).ToEnum<FileMode>());
            Assert.Null(((object?)null).ToEnum<FileAccess>());
            Assert.Null(((object?)null).ToEnum<DayOfWeek>());
        }

        [Fact]
        public void ToEnum_Object_Enum_To_Different_Enum_Should_Return_Null()
        {
            // Test conversion between different enum types
            var fileMode = FileMode.Open;
            var dayOfWeek = DayOfWeek.Monday;
            var fileAccess = FileAccess.Read;

            // These should all return null since they're different enum types
            Assert.Null(fileMode.ToEnum<DayOfWeek>());
            Assert.Null(fileMode.ToEnum<FileAccess>());
            Assert.Null(dayOfWeek.ToEnum<FileMode>());
            Assert.Null(dayOfWeek.ToEnum<FileAccess>());
            Assert.Null(fileAccess.ToEnum<FileMode>());
            Assert.Null(fileAccess.ToEnum<DayOfWeek>());
        }

        [Theory]
        [InlineData("invalid")]
        [InlineData("NotAValidEnum")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(999.99)] // Out of range double
        [InlineData(-1.5)] // Negative double
        public void ToEnum_Object_Invalid_Values_Should_Return_Null(object input)
        {
            Assert.Null(input.ToEnum<FileMode>());
            Assert.Null(input.ToEnum<FileAccess>());
            Assert.Null(input.ToEnum<DayOfWeek>());
        }

        [Fact]
        public void ToEnum_Object_Complex_Objects_Should_Delegate_To_ToString()
        {
            // Test with objects that have custom ToString() implementations
            var dateTime = new DateTime(2021, 1, 1);
            Assert.Null(dateTime.ToEnum<FileMode>()); // DateTime.ToString() won't match enum values

            var guid = Guid.NewGuid();
            Assert.Null(guid.ToEnum<FileMode>()); // Guid.ToString() won't match enum values
        }

        [Fact]
        public void ToEnum_Object_Boxed_Values_Should_Work()
        {
            // Test with boxed values
            object boxedInt = 3;
            object boxedString = "Open";
            object boxedEnum = FileMode.Open;

            Assert.Equal(FileMode.Open, boxedInt.ToEnum<FileMode>());
            Assert.Equal(FileMode.Open, boxedString.ToEnum<FileMode>());
            Assert.Equal(FileMode.Open, boxedEnum.ToEnum<FileMode>());
        }

        [Theory]
        [InlineData("ReadOnly", FileAttributes.ReadOnly)]
        [InlineData("readonly", FileAttributes.ReadOnly)]
        [InlineData("ReadOnly, Hidden", FileAttributes.ReadOnly | FileAttributes.Hidden)]
        [InlineData("ReadOnly, Hidden, System", FileAttributes.ReadOnly | FileAttributes.Hidden | FileAttributes.System)]
        public void ToEnum_Object_String_To_Flag_Enum_Should_Delegate_To_String_Method(string input, FileAttributes expected)
        {
            Assert.Equal(expected, input.ToEnum<FileAttributes>());
        }

        [Theory]
        [InlineData("ReadOnly|Hidden")]
        [InlineData("ReadOnly|Hidden|System")]
        public void ToEnum_Object_Invalid_Flag_Enum_Pipe_Separated_Should_Return_Null(string input)
        {
            Assert.Null(input.ToEnum<FileAttributes>());
        }

        #endregion
    }
}
