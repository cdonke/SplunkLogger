using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Splunk;
using Splunk.Extensions;

namespace MyLogger
{
    class Program
    {
        private static ServiceProvider _serviceProvider;
        private static ILogger<Program> _logger;
        private static EventsBag _eventsBag;

        static async Task Main(string[] args)
        {
            Configure();

            _logger = _serviceProvider.GetRequiredService<ILogger<Program>>();
            _eventsBag = _serviceProvider.GetRequiredService<EventsBag>();

            _logger.LogInformation("Hello world");
            await _eventsBag?.EnsureBagEmpty();

            Console.ReadLine();
        }

        static void Configure()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();
            services.AddLogging(o => o
                //.AddConfiguration(configuration)
                .AddConfiguration(configuration.GetSection("Logging"))
                .AddSplunkJsonLogger(configuration)
                //.AddSplunkRawLogger(configuration)
            );
            _serviceProvider = services.BuildServiceProvider();




        }
    }
}