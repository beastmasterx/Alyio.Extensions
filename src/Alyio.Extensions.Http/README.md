# Alyio.Extensions.Http

[![Build Status](https://travis-ci.org/qqbuby/Alyio.Extensions.Http.svg?branch=dev)](https://travis-ci.org/qqbuby/Alyio.Extensions.Http)

**Alyio..Extensions.Http** extends the `HttpClientHandler` for logging the HTTP request message and the HTTP response message.

To use the `HttpClientHandler`, please use `services.AddTransient<HttpClientLoggingHandler, HttpClientLoggingHandler>()` to add `HttpClientLoggingHandler` as an transient service to the specified `IServiceCollection`.

For example, the follow is a sample logging section.

```cs
var serviceProvider = new ServiceCollection()
    .AddLogging()
    .AddOptions()
    .AddTransient<HttpClientLoggingHandler, HttpClientLoggingHandler>()
    .BuildServiceProvider();

serviceProvider.GetService<ILoggerFactory>().AddConsole(minLevel: LogLevel.Debug);

var handler = serviceProvider.GetService<HttpClientLoggingHandler>();
// handler.LoggerCategoryName = this.GetType().FullName;   // customization for setting up logger category name.
// handler.LoggingOptions.IsEnabled = true;
// handler.LoggingOptions.IgnoreRequestContent = true;
// handler.LoggingOptions.IgnoreRequestHeaders = new[] { "Location" };
// handler.LoggingOptions.IgnoreResponseContent = true;
// handler.LoggingOptions.IgnoreResponseHeaders = new[] { "Location", "Cache-Control", "Connection" };
var client = new HttpClient(handler) { BaseAddress = new Uri("https://api.github.com/") };

var get = await client.GetAsync("/");
```

```none
info: Alyio.Extensions.Http.HttpClientLoggingHandler[0]
      Request-Queue: 1
info: Alyio.Extensions.Http.HttpClientLoggingHandler[0]
      Request-Message:

      DELETE https://api.github.com/ HTTP/1.1

info: Alyio.Extensions.Http.HttpClientLoggingHandler[0]
      Response-Message: 310ms

      HTTP/1.0 403 Forbidden
      Cache-Control: no-cache
      Connection: close
      Content-Type: text/html
```
