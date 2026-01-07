using Aster.Net.Clients.FuturesApi;
using Aster.Net.Clients.SpotApi;
using Aster.Net.Objects.Internal;
using Aster.Net.Objects.Models;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Microsoft.Extensions.Logging;
using System;

namespace Aster.Net.Objects.Sockets
{
    /// <inheritdoc />
    internal class AsterSpotUserDataSubscription : Subscription
    {
        private readonly string _lk;
        private readonly AsterSocketClientSpotApi _client;

        private readonly Action<DataEvent<AsterSpotOrderUpdate>>? _orderHandler;
        private readonly Action<DataEvent<AsterSpotAccountUpdate>>? _accountHandler;

        /// <summary>
        /// ctor
        /// </summary>
        public AsterSpotUserDataSubscription(
            ILogger logger,
            AsterSocketClientSpotApi client,
            string listenKey,
            Action<DataEvent<AsterSpotOrderUpdate>>? orderHandler,
            Action<DataEvent<AsterSpotAccountUpdate>>? accountHandler) : base(logger, false)
        {
            _client = client;

            _orderHandler = orderHandler;
            _accountHandler = accountHandler;

            _lk = listenKey;

            MessageRouter = MessageRouter.Create([
                MessageRoute<AsterCombinedStream<AsterSpotAccountUpdate>>.CreateWithoutTopicFilter("outboundAccountPosition", DoHandleMessage),
                MessageRoute<AsterCombinedStream<AsterSpotOrderUpdate>>.CreateWithoutTopicFilter("executionReport", DoHandleMessage)
                ]);

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

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, AsterCombinedStream<AsterSpotAccountUpdate> message)
        {
            _client.UpdateTimeOffset(message.Data.EventTime);

            message.Data.ListenKey = message.Stream;
            _accountHandler?.Invoke(
                new DataEvent<AsterSpotAccountUpdate>(AsterExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime, _client.GetTimeOffset())
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, AsterCombinedStream<AsterSpotOrderUpdate> message)
        {
            _client.UpdateTimeOffset(message.Data.EventTime);

            message.Data.ListenKey = message.Stream;
            _orderHandler?.Invoke(
                new DataEvent<AsterSpotOrderUpdate>(AsterExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithSymbol(message.Data.Symbol)
                    .WithDataTimestamp(message.Data.EventTime, _client.GetTimeOffset())
                );
            return CallResult.SuccessResult;
        }
    }
}
