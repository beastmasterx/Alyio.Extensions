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
        [InlineData("hello", "hello")]
        [InlineData(123, "123")]
        [InlineData(null, "")]
        public void ToStringOrEmpty_Should_Return_Correct_String(object? input, string expected)
        {
            Assert.Equal(expected, input.ToStringOrEmpty());
        }

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
        [InlineData(999)] // Out of range
        [InlineData(-1)] // Negative
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
