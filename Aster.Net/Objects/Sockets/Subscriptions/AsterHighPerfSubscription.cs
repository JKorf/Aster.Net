using Aster.Net.Objects.Internal;
using Aster.Net.Objects.Sockets;
using CryptoExchange.Net;
using CryptoExchange.Net.Sockets.HighPerf;
using System;

namespace Aster.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class AsterHighPerfSubscription<T> : HighPerfSubscription<T>
    {
        private string[] _params;

        /// <summary>
        /// ctor
        /// </summary>
        public AsterHighPerfSubscription(string[] topics, Action<T> handler) : base(handler)
        {
            _params = topics;
        }

        /// <inheritdoc />
        protected override object? GetSubQuery(HighPerfSocketConnection connection)
        {
            return new AsterSocketRequest
            {
                Method = "SUBSCRIBE",
                Params = _params,
                Id = ExchangeHelpers.NextId()
            };
        }
    }
}
