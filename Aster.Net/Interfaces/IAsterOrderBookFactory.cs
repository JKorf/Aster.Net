using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.SharedApis;
using System;
using Aster.Net.Objects.Options;

namespace Aster.Net.Interfaces
{
    /// <summary>
    /// Aster local order book factory
    /// </summary>
    public interface IAsterOrderBookFactory
    {
        
        /// <summary>
        /// Futures order book factory methods
        /// </summary>
        IOrderBookFactory<AsterOrderBookOptions> Futures { get; }


        /// <summary>
        /// Create a SymbolOrderBook for the symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Book options</param>
        /// <returns></returns>
        ISymbolOrderBook Create(SharedSymbol symbol, Action<AsterOrderBookOptions>? options = null);

        
        /// <summary>
        /// Create a new Futures local order book instance
        /// </summary>
        ISymbolOrderBook CreateFutures(string symbol, Action<AsterOrderBookOptions>? options = null);

    }
}