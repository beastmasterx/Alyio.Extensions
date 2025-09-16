// MIT License

using Xunit;

namespace Alyio.Extensions.Tests
{
    internal sealed class TestData
    {
        public static TheoryData<string?, decimal?> StringToDecimal => new()
        {
            { "9527.5", 9527.5m },
            { "-100.5", -100.5m },
            { "0.0", 0.0m },
            { null, null },
            { "", null },
            { " ", null },
            { "x", null }
        };

        public static TheoryData<object?, decimal?> ObjectToDecimal => new() {
            { "9527.5", 9527.5m },
            { 9527.5, 9527.5m },
            { 9527, 9527.0m },
            { 0, 0.0m },
            { null, null },
            { "", null },
            { " ", null },
            { "x", null }
        };
    }
}
