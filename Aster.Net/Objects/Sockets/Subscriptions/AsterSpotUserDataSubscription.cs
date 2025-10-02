using Aster.Net.Objects.Internal;
using Aster.Net.Objects.Models;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System;

namespace Aster.Net.Objects.Sockets
{
    /// <inheritdoc />
    internal class AsterSpotUserDataSubscription : Subscription<AsterSocketQueryResponse, AsterSocketQueryResponse>
    {
        private readonly string _lk;

        private readonly Action<DataEvent<AsterSpotOrderUpdate>>? _orderHandler;
        private readonly Action<DataEvent<AsterSpotAccountUpdate>>? _accountHandler;

        /// <summary>
        /// ctor
        /// </summary>
        public AsterSpotUserDataSubscription(
            ILogger logger,
            string listenKey,
            Action<DataEvent<AsterSpotOrderUpdate>>? orderHandler,
            Action<DataEvent<AsterSpotAccountUpdate>>? accountHandler) : base(logger, false)
        {
            _orderHandler = orderHandler;
            _accountHandler = accountHandler;

            _lk = listenKey;

            MessageMatcher = MessageMatcher.Create([
                new MessageHandlerLink<AsterCombinedStream<AsterSpotAccountUpdate>>(_lk + "outboundAccountPosition", DoHandleMessage),
                new MessageHandlerLink<AsterCombinedStream<AsterSpotOrderUpdate>>(_lk + "executionReport", DoHandleMessage)
                ]);
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new AsterSystemQuery<AsterSocketQueryResponse>(new AsterSocketRequest
            {
                Method = "SUBSCRIBE",
                Params = [_lk],
                Id = ExchangeHelpers.NextId()
            }, false);
        }

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new AsterSystemQuery<AsterSocketQueryResponse>(new AsterSocketRequest
            {
                Method = "UNSUBSCRIBE",
                Params = [_lk],
                Id = ExchangeHelpers.NextId()
            }, false);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<AsterCombinedStream<AsterSpotAccountUpdate>> message)
        {
            message.Data.Data.ListenKey = message.Data.Stream;
            _accountHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<AsterCombinedStream<AsterSpotOrderUpdate>> message)
        {
            message.Data.Data.ListenKey = message.Data.Stream;
            _orderHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, message.Data.Data.Symbol, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            return CallResult.SuccessResult;
        }
    }
}
