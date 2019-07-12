using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Alyio.Extensions.DistributedCache.Json
{
    /// <summary>
    /// Extension methods for <see cref="IDistributedCache"/>.
    /// </summary>
    public static partial class DistributedCacheExtensions
    {
        private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

        private static byte[] Serialize<T>(T data)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data, _jsonSerializerSettings));
        }

        private static T Deserialize<T>(byte[] bytes)
        {
            var jsonText = Encoding.UTF8.GetString(bytes);
            return JsonConvert.DeserializeObject<T>(jsonText);
        }
    }
}
