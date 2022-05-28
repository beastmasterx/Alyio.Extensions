using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Alyio.Extensions.Http
{
    /// <summary>
    /// The message handler used by <see cref="HttpClient"/> for logging http request and response message.
    /// </summary>
    public sealed class HttpLoggingHandler : DelegatingHandler
    {
        private readonly string _DOUBLE_NEW_LINE = Environment.NewLine + Environment.NewLine;

        private readonly ILoggerFactory _loggerFactory;

        private int _requestCount;
        private ILogger _logger;
        private string _loggerCategoryName;

        /// <summary>
        /// Gets or sets the category name for the logging handler.
        /// </summary>
        /// <remarks>The default is the the fully qualified name of the <see cref="HttpLoggingHandler"/>, including the namespace of the <see cref="HttpLoggingHandler"/> but not the assembly.</remarks>
        public string LoggerCategoryName
        {
            get { return _loggerCategoryName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _loggerCategoryName = value;
                _logger = _loggerFactory.CreateLogger(_loggerCategoryName);
            }
        }

        /// <summary>
        /// Gets the http logging options.
        /// </summary>
        public HttpLoggingOptions LoggingOptions { get; } = new HttpLoggingOptions();

        /// <summary>
        /// Creates an instance of a <see cref="HttpLoggingHandler"/> class.
        /// </summary>
        /// <param name="loggerFactory">The <see cref="ILoggerFactory"/>.</param>
        public HttpLoggingHandler(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _loggerCategoryName = this.GetType().FullName;
            _logger = _loggerFactory.CreateLogger(_loggerCategoryName);
        }

        /// <summary>
        /// Creates an instance of <see cref="HttpResponseMessage"/> based on the information provided in the <see cref="HttpRequestMessage"/> as an operation that will not block.
        /// </summary>
        /// <param name="request">The HTTP request message.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
        /// <returns>Returns <see cref="Task{TResult}"/>. The task object representing the asynchronous operation.</returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (this.LoggingOptions.IsEnabled)
            {
                return SendCoreAsync(request, cancellationToken);
            }
            else
            {
                return base.SendAsync(request, cancellationToken);
            }
        }

        private async Task<HttpResponseMessage> SendCoreAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Request-Queue: {Interlocked.Increment(ref _requestCount)}");

            Stopwatch watch = Stopwatch.StartNew();

            string requestRawMessage = await request.ReadRawMessageAsync(LoggingOptions.IgnoreRequestContent, LoggingOptions.IgnoreRequestHeaders);

            HttpResponseMessage? responseMessage = null;

            try
            {
                _logger.LogInformation("Request-Message: {NewLine}{RequestRawMessage}", _DOUBLE_NEW_LINE, requestRawMessage);

                responseMessage = await base.SendAsync(request, cancellationToken);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Request-Error: {Message}, elapsed: {ElapsedMilliseconds}ms{NewLine}{RequestRawMessage}",
                    ex.Message,
                    watch.ElapsedMilliseconds,
                    _DOUBLE_NEW_LINE,
                    requestRawMessage);

                throw;
            }
            finally
            {
                Interlocked.Decrement(ref _requestCount);
            }

            var responseRawMessage = await responseMessage.ReadRawMessageAsync(LoggingOptions.IgnoreResponseContent, LoggingOptions.IgnoreResponseHeaders);
            _logger.LogInformation("Response-Message: {Elapsed}ms{NewLine}{ResponseRawMessage}", watch.ElapsedMilliseconds, _DOUBLE_NEW_LINE, responseRawMessage);

            return await Task.FromResult(responseMessage);
        }
    }
}
