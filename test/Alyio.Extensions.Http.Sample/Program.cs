using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Alyio.Extensions.Http.Sample
{
    internal static class Program
    {
        static void Main()
        {
            var host = new HostBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<HttpLoggingHandler>();

                    services.AddHttpClient<IOpenWeatherMapService, OpenWeatherMapService>(configureClient => configureClient.BaseAddress = new Uri("http://samples.openweathermap.org"))
                    .AddHttpMessageHandler((sp) =>
                    {
                        var handler = sp.GetService<HttpLoggingHandler>();
                        handler.LoggerCategoryName = typeof(OpenWeatherMapHostedService).FullName;
                        handler.LoggingOptions.IgnoreRequestContent = false;
                        handler.LoggingOptions.IgnoreResponseContent = false;
                        return handler;
                    });

                    services.AddHostedService<OpenWeatherMapHostedService>();
                })
                .ConfigureLogging((context, configLogging) =>
                {
                    configLogging.AddConsole();
                    configLogging.AddDebug();
                })
                .UseConsoleLifetime();

            host.RunConsoleAsync().Wait();
        }
    }

    sealed class OpenWeatherMapHostedService : IHostedService
    {
        private readonly IOpenWeatherMapService _openWeatherMap;

        public OpenWeatherMapHostedService(IOpenWeatherMapService weatherMapService)
        {
            _openWeatherMap = weatherMapService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return _openWeatherMap.GetAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    internal interface IOpenWeatherMapService
    {
        Task GetAsync();
    }

    sealed class OpenWeatherMapService : IOpenWeatherMapService
    {
        private readonly HttpClient _client;

        public OpenWeatherMapService(HttpClient client)
        {
            _client = client;
        }

        public Task GetAsync()
        {
            return _client.GetAsync("/data/2.5/weather?q=London,uk&appid=b1b15e88fa797225412429c1c50c122a1");
        }
    }
}
