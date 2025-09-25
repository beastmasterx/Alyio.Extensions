// MIT License

using System.Globalization;
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
        public void ToDateTimeOffset_Should_Convert_Correctly()
        {
            // Null, empty, or whitespace input
            Assert.Null(((string?)null).ToDateTimeOffset());
            Assert.Null("".ToDateTimeOffset());
            Assert.Null(" ".ToDateTimeOffset());

            // Valid string inputs with offset
            var expectedDto = new DateTimeOffset(2006, 1, 2, 15, 4, 5, TimeSpan.FromHours(2));
            Assert.Equal(expectedDto, "2006-01-02T15:04:05+02:00".ToDateTimeOffset(styles: DateTimeStyles.None, provider: CultureInfo.InvariantCulture));
            Assert.Equal(expectedDto, "01/02/2006 3:04:05 PM +02:00".ToDateTimeOffset(styles: DateTimeStyles.None, provider: CultureInfo.InvariantCulture));

            // Valid string inputs without offset, assuming local
            var dateStringNoOffset = "2006-01-02 15:04:05";
            var expectedDtoLocal = new DateTimeOffset(new DateTime(2006, 1, 2, 15, 4, 5), TimeZoneInfo.Local.GetUtcOffset(new DateTime(2006, 1, 2, 15, 4, 5)));
            Assert.Equal(expectedDtoLocal, dateStringNoOffset.ToDateTimeOffset(styles: DateTimeStyles.None, provider: CultureInfo.InvariantCulture));

            // Valid string inputs without offset, assuming UTC
            var dateStringAssumeUtc = "2006-01-02 15:04:05";
            var expectedDtoUtc = new DateTimeOffset(2006, 1, 2, 15, 4, 5, TimeSpan.Zero);
            Assert.Equal(expectedDtoUtc, dateStringAssumeUtc.ToDateTimeOffset(styles: DateTimeStyles.AssumeUniversal, provider: CultureInfo.InvariantCulture));

            // Invalid string input
            Assert.Null("invalid date".ToDateTimeOffset(styles: DateTimeStyles.None, provider: CultureInfo.InvariantCulture));
            Assert.Null("2006-13-01".ToDateTimeOffset(styles: DateTimeStyles.None, provider: CultureInfo.InvariantCulture)); // Invalid month
        }

#if NET5_0_OR_GREATER
        [Theory]
        [InlineData("2006-01-02", 2006, 1, 2)]
        [InlineData("01/02/2006", 2006, 1, 2)]
        [InlineData("2006-01-02 15:04:05", 2006, 1, 2)] // Time should be omitted
        [InlineData("January 2, 2006", 2006, 1, 2)]
        public void ToDateOnly_String_Valid_Should_Convert_Correctly(string input, int year, int month, int day)
        {
            var expected = new DateOnly(year, month, day);
            Assert.Equal(expected, input.ToDateOnly(CultureInfo.InvariantCulture));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("invalid date")]
        [InlineData("2023-13-01")] // Invalid month
        public void ToDateOnly_String_Invalid_Should_Return_Null(string? input)
        {
            Assert.Null(input.ToDateOnly(CultureInfo.InvariantCulture));
        }
#endif

        #region ToEnum Tests

        [Theory]
        [InlineData("Read", FileAccess.Read)]
        [InlineData("read", FileAccess.Read)]
        [InlineData("READ", FileAccess.Read)]
        [InlineData("Write", FileAccess.Write)]
        [InlineData("ReadWrite", FileAccess.ReadWrite)]
        [InlineData("readwrite", FileAccess.ReadWrite)]
        public void ToEnum_String_To_FileAccess_Should_Convert_Correctly(string input, FileAccess expected)
        {
            Assert.Equal(expected, input.ToEnum<FileAccess>());
        }

        [Theory]
        [InlineData("1", FileMode.CreateNew)]
        [InlineData("2", FileMode.Create)]
        [InlineData("3", FileMode.Open)]
        public void ToEnum_String_Numeric_To_FileMode_Should_Convert_Correctly(string input, FileMode expected)
        {
            Assert.Equal(expected, input.ToEnum<FileMode>());
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData("\t")]
        [InlineData("\n")]
        [InlineData("\r\n")]
        [InlineData(null)]
        public void ToEnum_String_Empty_Or_Whitespace_Should_Return_Null(string? input)
        {
            Assert.Null(input.ToEnum<FileMode>());
            Assert.Null(input.ToEnum<FileAccess>());
            Assert.Null(input.ToEnum<DayOfWeek>());
        }

        [Theory]
        [InlineData("invalid")]
        [InlineData("NotAValidEnum")]
        [InlineData("OpenInvalid")]
        [InlineData("123abc")]
        [InlineData("abc123")]
        [InlineData("!@#$%")]
        [InlineData("Open|Create")] // Bitwise OR
        public void ToEnum_String_Invalid_Should_Return_Null(string input)
        {
            Assert.Null(input.ToEnum<FileMode>());
            Assert.Null(input.ToEnum<FileAccess>());
            Assert.Null(input.ToEnum<DayOfWeek>());
        }

        [Fact]
        public void ToEnum_String_With_Flags_Enum_Should_Work()
        {
            // Test with a flags enum if available, or create a simple test
            // For now, we'll test with existing enums
            Assert.Equal(FileMode.Open, "Open".ToEnum<FileMode>());
            Assert.Equal(FileAccess.Read, "Read".ToEnum<FileAccess>());
        }

        [Theory]
        [InlineData("Open", FileMode.Open)]
        [InlineData("open", FileMode.Open)]
        [InlineData("OPEN", FileMode.Open)]
        [InlineData("oPeN", FileMode.Open)]
        [InlineData("OpEn", FileMode.Open)]
        public void ToEnum_String_Case_Insensitive_Should_Work(string input, FileMode expected)
        {
            Assert.Equal(expected, input.ToEnum<FileMode>());
        }

        [Theory]
        [InlineData("ReadOnly, Hidden", FileAttributes.ReadOnly | FileAttributes.Hidden)]
        [InlineData("readonly, hidden", FileAttributes.ReadOnly | FileAttributes.Hidden)]
        [InlineData("READONLY, HIDDEN", FileAttributes.ReadOnly | FileAttributes.Hidden)]
        [InlineData("ReadOnly, Hidden, System", FileAttributes.ReadOnly | FileAttributes.Hidden | FileAttributes.System)]
        [InlineData("Archive, Compressed", FileAttributes.Archive | FileAttributes.Compressed)]
        [InlineData("Directory, ReadOnly", FileAttributes.Directory | FileAttributes.ReadOnly)]
        public void ToEnum_String_Flag_Enum_Comma_Separated_Should_Convert_Correctly(string input, FileAttributes expected)
        {
            Assert.Equal(expected, input.ToEnum<FileAttributes>());
        }

        [Theory]
        [InlineData("ReadOnly|Hidden")]
        [InlineData("readonly|hidden")]
        [InlineData("READONLY|HIDDEN")]
        [InlineData("ReadOnly|Hidden|System")]
        [InlineData("Archive|Compressed")]
        [InlineData("Directory|ReadOnly")]
        public void ToEnum_String_Flag_Enum_Pipe_Separated_Should_Return_Null(string input)
        {
            Assert.Null(input.ToEnum<FileAttributes>());
        }

        #endregion
    }
}
