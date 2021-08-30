using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Splunk.Configurations;
using Splunk.Providers;

namespace Splunk.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static ILoggingBuilder AddSplunkJsonLogger(this ILoggingBuilder builder, IConfiguration configuration,
            ILoggerFormatter formatter = null)
        {
            builder.Services.TryAdd(ServiceDescriptor.Singleton<ILoggerFactory, LoggerFactory>());
            builder.Services.TryAdd(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(Logger<>)));


            builder.Services.TryAdd(ServiceDescriptor.Singleton<ILoggerFormatter>((sp) => formatter ?? new BasicLoggerFormatter()));
            builder.Services.TryAdd(ServiceDescriptor.Singleton<EventsBag, EventsBag>());

            builder.Services.TryAdd(ServiceDescriptor.Singleton<SplunkLoggerConfiguration>((obj) =>
                configuration.GetSection("Splunk").Get<SplunkLoggerConfiguration>()
            ));


            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, SplunkHECJsonLoggerProvider>());


            return builder;
        }

        public static ILoggingBuilder AddSplunkRawLogger(this ILoggingBuilder builder, IConfiguration configuration,
            ILoggerFormatter formatter = null)
        {
            builder.Services.TryAdd(ServiceDescriptor.Singleton<ILoggerFactory, LoggerFactory>());
            builder.Services.TryAdd(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(Logger<>)));


            builder.Services.TryAdd(ServiceDescriptor.Singleton<ILoggerFormatter>((sp) => formatter ?? new BasicLoggerFormatter()));
            builder.Services.TryAdd(ServiceDescriptor.Singleton<EventsBag, EventsBag>());

            builder.Services.TryAdd(ServiceDescriptor.Singleton<SplunkLoggerConfiguration>((obj) =>
                configuration.GetSection("Splunk").Get<SplunkLoggerConfiguration>()
            ));


            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, SplunkHECRawLoggerProvider>());


            return builder;
        }
    }
}
