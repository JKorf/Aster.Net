using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Aster.Net.Interfaces;
using Aster.Net.Interfaces.Clients;
using Aster.Net.Objects.Options;

namespace Aster.Net.SymbolOrderBooks
{
    /// <summary>
    /// Aster order book factory
    /// </summary>
    public class AsterOrderBookFactory : IAsterOrderBookFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <inheritdoc />
        public string ExchangeName => AsterExchange.ExchangeName;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public AsterOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;            
            
            Futures = new OrderBookFactory<AsterOrderBookOptions>(CreateFutures, Create);
            Spot = new OrderBookFactory<AsterOrderBookOptions>(CreateSpot, Create);
        }

        /// <inheritdoc />
        public IOrderBookFactory<AsterOrderBookOptions> Spot { get; }

        /// <inheritdoc />
        public IOrderBookFactory<AsterOrderBookOptions> Futures { get; }

        /// <inheritdoc />
        public ISymbolOrderBook Create(SharedSymbol symbol, Action<AsterOrderBookOptions>? options = null)
        {
            var symbolName = symbol.GetSymbol(AsterExchange.FormatSymbol);

            if (symbol.TradingMode == TradingMode.Spot)
                return CreateSpot(symbolName, options);

            return CreateFutures(symbolName, options);
        }

        /// <inheritdoc />
        public ISymbolOrderBook CreateSpot(string symbol, Action<AsterOrderBookOptions>? options = null)
            => new AsterSpotSymbolOrderBook(symbol, options,
                                                          _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                                          _serviceProvider.GetRequiredService<IAsterRestClient>(),
                                                          _serviceProvider.GetRequiredService<IAsterSocketClient>());

        /// <inheritdoc />
        public ISymbolOrderBook CreateFutures(string symbol, Action<AsterOrderBookOptions>? options = null)
            => new AsterFuturesSymbolOrderBook(symbol, options, 
                                                          _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                                          _serviceProvider.GetRequiredService<IAsterRestClient>(),
                                                          _serviceProvider.GetRequiredService<IAsterSocketClient>());


    }
}
