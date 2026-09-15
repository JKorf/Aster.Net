using Aster.Net;
using Aster.Net.Clients;
using Aster.Net.Interfaces;
using Aster.Net.Interfaces.Clients;
using Aster.Net.Objects.Options;
using Aster.Net.SymbolOrderBooks;
using CryptoExchange.Net;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add services such as the IAsterRestClient and IAsterSocketClient. Configures the services based on the provided configuration.<br />
        /// See <see href="https://github.com/JKorf/Aster.Net/blob/main/Examples/example-config.json" /> for an example of how to set up the configuration.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration(section) containing the options</param>
        /// <returns></returns>
        public static IServiceCollection AddAster(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = AsterOptions.CreateFromConfiguration(configuration);

            services.AddSingleton(Options.Options.Create(options.Rest));
            services.AddSingleton(Options.Options.Create(options.Socket));
            services.AddSingleton(Options.Options.Create(options));

            return AddAsterCore(services, AsterApiVersion.V3, options.SocketClientLifeTime);
        }

        /// <summary>
        /// Add services such as the IAsterRestClient and IAsterSocketClient. Services will be configured based on the provided options.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="optionsDelegate">Set options for the Aster services</param>
        /// <returns></returns>
        public static IServiceCollection AddAster(
            this IServiceCollection services,
            Action<AsterOptions>? optionsDelegate = null)
        {
            var options = AsterOptions.Create(optionsDelegate);

            services.AddSingleton(Options.Options.Create(options.Rest));
            services.AddSingleton(Options.Options.Create(options.Socket));
            services.AddSingleton(Options.Options.Create(options));

            return AddAsterCore(services, options.SharedApi.ApiVersion, options.SocketClientLifeTime);
        }

        private static IServiceCollection AddAsterCore(
            this IServiceCollection services,
            AsterApiVersion version,
            ServiceLifetime? socketClientLifeTime = null)
        {
            services.AddHttpClient<IAsterRestClient, AsterRestClient>((client, serviceProvider) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<AsterRestOptions>>().Value;
                client.Timeout = options.RequestTimeout;
                return new AsterRestClient(client, serviceProvider.GetRequiredService<ILoggerFactory>(), serviceProvider.GetRequiredService<IOptions<AsterRestOptions>>());
            }).ConfigurePrimaryHttpMessageHandler((serviceProvider) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<AsterRestOptions>>().Value;
                return LibraryHelpers.CreateHttpClientMessageHandler(options);
            }).SetHandlerLifetime(Timeout.InfiniteTimeSpan);
            services.Add(new ServiceDescriptor(typeof(IAsterSocketClient), x => { return new AsterSocketClient(x.GetRequiredService<IOptions<AsterSocketOptions>>(), x.GetRequiredService<ILoggerFactory>()); }, socketClientLifeTime ?? ServiceLifetime.Singleton));

            services.AddTransient<IAsterOrderBookFactory, AsterOrderBookFactory>();
            services.AddTransient<IAsterTrackerFactory, AsterTrackerFactory>();
            services.AddTransient<ITrackerFactory, AsterTrackerFactory>();
            services.AddSingleton<IAsterUserClientProvider, AsterUserClientProvider>(x =>
                new AsterUserClientProvider(
                    x.GetRequiredService<IHttpClientFactory>().CreateClient(typeof(IAsterRestClient).Name),
                    x.GetRequiredService<ILoggerFactory>(),
                    x.GetRequiredService<IOptions<AsterRestOptions>>(),
                    x.GetRequiredService<IOptions<AsterSocketOptions>>()));

            if (version == AsterApiVersion.V3)
            {
                services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IAsterRestClient>().SpotV3Api.SharedClient);
                services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IAsterRestClient>().FuturesV3Api.SharedClient);
            }
            else
            {
                services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IAsterRestClient>().SpotApi.SharedClient);
                services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IAsterRestClient>().FuturesApi.SharedClient);
            }

            services.RegisterSharedApiClient<
                IAsterSharedApiClient,
                AsterSharedApiClient>(sharedApis =>
                {
                    if (version == AsterApiVersion.V3)
                    {
                        sharedApis
                            .Add(client => client.SpotV3Rest)
                            .Add(client => client.SpotV3Socket)
                            .Add(client => client.FuturesV3Rest)
                            .Add(client => client.FuturesV3Socket);

                    }
                    else
                    {
                        sharedApis
                            .Add(client => client.SpotRest)
                            .Add(client => client.SpotSocket)
                            .Add(client => client.FuturesRest)
                            .Add(client => client.FuturesSocket);
                    }
                });

            return services;
        }
    }
}
