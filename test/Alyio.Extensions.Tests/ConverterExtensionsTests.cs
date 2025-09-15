// MIT License

using System.Globalization;
using Xunit;

namespace Alyio.Extensions.Tests
{
    public class ConverterExtensionsTests
    {
        private readonly string NaN = "zhangsan";
        private readonly object Unconvertible_NaN = new object();
        private readonly object Unconvertible_Num = new TestNum();

        private readonly object NaDate = "lisi";
        private readonly object Unconvertible_NaDate = new object();

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "<Pending>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "<Pending>")]
        public void ConverterExtension_Tests()
        {
            Assert.Null(NaN.ToInt32());
            Assert.Null(Unconvertible_NaN.ToInt32());
            Assert.NotNull(Unconvertible_Num.ToInt32());
            Assert.Equal(9527, Unconvertible_Num.ToInt32());

            Assert.Null(NaN.ToInt64());
            Assert.Null(Unconvertible_NaN.ToInt64());
            Assert.NotNull(Unconvertible_Num.ToInt64());
            Assert.Equal(9527L, Unconvertible_Num.ToInt64());

            Assert.Null(NaN.ToDouble());
            Assert.Null(Unconvertible_NaN.ToDouble());
            Assert.NotNull(Unconvertible_Num.ToDouble());
            Assert.Equal(9527D, Unconvertible_Num.ToDouble());

            Assert.Null(NaN.ToDecimal());
            Assert.Null(Unconvertible_NaN.ToDecimal());
            Assert.NotNull(Unconvertible_Num.ToDecimal());
            Assert.Equal(9527M, Unconvertible_Num.ToDecimal());

            Assert.Null(NaDate.ToDateTime());
            Assert.Null(Unconvertible_NaDate.ToDateTime());

            DateTime date = DateTime.Parse(DateTime.Now.ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
            var unconvertible_Date = new TestDate(date);
            Assert.NotNull(unconvertible_Date.ToDateTime());
            Assert.Equal(date, unconvertible_Date.ToDateTime());
            Assert.Equal(date.Date, unconvertible_Date.ToDate());

            var fileMode = FileMode.Open;
            Assert.Equal(FileMode.Open, fileMode.ToEnum<FileMode>());
            Assert.Equal(FileMode.Open, 3.ToEnum<FileMode>());
            Assert.Equal(FileMode.Open, "3".ToEnum<FileMode>());
            Assert.Equal(FileMode.Open, "Open".ToEnum<FileMode>());
            Assert.Equal(FileMode.Open, "OPEN".ToEnum<FileMode>());
        }

        private sealed class TestNum
        {
            public override string ToString()
            {
                return "9527";
            }
        }

        private sealed class TestDate
        {
            private readonly DateTime _date;

            public TestDate(DateTime date)
            {
                _date = date;
            }

            public override string ToString()
            {
                return _date.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}
