namespace Alyio.DistributedCacheExtensions.Json.Tests;

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

public class DistributedCacheExtensionsTests
{
    [Fact]
    public Task Test_Set_Get_Async()
    {
        using var services = new ServiceCollection().AddDistributedMemoryCache().BuildServiceProvider();
        var cache = services.GetRequiredService<IDistributedCache>;

        return Task.FromResult(0);
    }
}