using System.Net.Http;

namespace Alyio.Extensions.Http
{
    /// <summary>
    /// Provides programmatic configuration for http logging handler.
    /// </summary>
    public sealed class HttpLoggingOptions
    {
        /// <summary>
        /// Gets or sets a <see cref="bool"/> value that whether the logger is enalble; The default is true.
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets a <see cref="bool"/> value that indicates to ignore the request content. The default is true.
        /// </summary>
        public bool IgnoreRequestContent { get; set; } = true;

        /// <summary>
        /// Gets or sets a <see cref="string"/> array to ignore the specified headers of <see cref="HttpRequestMessage.Headers"/>.
        /// </summary>
        public string[] IgnoreRequestHeaders { get; set; } = new string[] { };

        /// <summary>
        /// Gets or sets a <see cref="bool"/> value that indicates to ignore the response content. The default is true.
        /// </summary>
        public bool IgnoreResponseContent { get; set; } = true;

        /// <summary>
        /// Gets or sets a <see cref="string"/> array to ignore the specified headers of <see cref="HttpResponseMessage.Headers"/>.
        /// </summary>
        public string[] IgnoreResponseHeaders { get; set; } = new string[] { };
    }
}
