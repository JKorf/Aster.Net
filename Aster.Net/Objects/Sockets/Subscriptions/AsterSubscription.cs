using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Aster.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class AsterSubscription<T> : Subscription
    {
        private readonly Action<DateTime, string?, T> _handler;
        private string[] _params;

        /// <summary>
        /// ctor
        /// </summary>
        public AsterSubscription(ILogger logger, string dataType, List<string> topics, Action<DateTime, string?, T> handler, bool auth) : base(logger, auth)
        {
            _handler = handler;
            _params = topics.ToArray();

            IndividualSubscriptionCount = topics.Count;

            MessageMatcher = MessageMatcher.Create<T>(topics, DoHandleMessage);
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<T>(topics, DoHandleMessage);
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new AsterSystemQuery<AsterSocketQueryResponse>(new AsterSocketRequest
            {
                Method = "SUBSCRIBE",
                Params = _params,
                Id = ExchangeHelpers.NextId()
            }, false);
        }

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new AsterSystemQuery<AsterSocketQueryResponse>(new AsterSocketRequest
            {
                Method = "UNSUBSCRIBE",
                Params = _params,
                Id = ExchangeHelpers.NextId()
            }, false);
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, T message)
        {
            _handler.Invoke(receiveTime, originalData, message);
            return CallResult.SuccessResult;
        }
    }
}
