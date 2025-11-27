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
    internal class AsterUserDataSubscription : Subscription<AsterSocketQueryResponse, AsterSocketQueryResponse>
    {
        private readonly string _lk;

        private readonly Action<DataEvent<AsterOrderUpdate>>? _orderHandler;
        private readonly Action<DataEvent<AsterConfigUpdate>>? _configHandler;
        private readonly Action<DataEvent<AsterMarginUpdate>>? _marginHandler;
        private readonly Action<DataEvent<AsterAccountUpdate>>? _accountHandler;
        private readonly Action<DataEvent<AsterSocketEvent>>? _listenkeyHandler;

        /// <summary>
        /// ctor
        /// </summary>
        public AsterUserDataSubscription(
            ILogger logger,
            string listenKey,
            Action<DataEvent<AsterOrderUpdate>>? orderHandler,
            Action<DataEvent<AsterConfigUpdate>>? configHandler,
            Action<DataEvent<AsterMarginUpdate>>? marginHandler,
            Action<DataEvent<AsterAccountUpdate>>? accountHandler,
            Action<DataEvent<AsterSocketEvent>>? listenkeyHandler) : base(logger, false)
        {
            _orderHandler = orderHandler;
            _configHandler = configHandler;
            _marginHandler = marginHandler;
            _accountHandler = accountHandler;
            _listenkeyHandler = listenkeyHandler;

            _lk = listenKey;

            MessageMatcher = MessageMatcher.Create([
                new MessageHandlerLink<AsterCombinedStream<AsterConfigUpdate>>(_lk + "ACCOUNT_CONFIG_UPDATE", DoHandleMessage),
                new MessageHandlerLink<AsterCombinedStream<AsterMarginUpdate>>(_lk + "MARGIN_CALL", DoHandleMessage),
                new MessageHandlerLink<AsterCombinedStream<AsterAccountUpdate>>(_lk + "ACCOUNT_UPDATE", DoHandleMessage),
                new MessageHandlerLink<AsterCombinedStream<AsterOrderUpdate>>(_lk + "ORDER_TRADE_UPDATE", DoHandleMessage),
                new MessageHandlerLink<AsterCombinedStream<AsterSocketEvent>>(_lk + "listenKeyExpired", DoHandleMessage)
                ]);

            MessageRouter = MessageRouter.Create([
                new MessageRoute<AsterCombinedStream<AsterConfigUpdate>>("ACCOUNT_CONFIG_UPDATE", _lk, DoHandleMessage),
                new MessageRoute<AsterCombinedStream<AsterMarginUpdate>>("MARGIN_CALL", _lk, DoHandleMessage),
                new MessageRoute<AsterCombinedStream<AsterAccountUpdate>>("ACCOUNT_UPDATE", _lk, DoHandleMessage),
                new MessageRoute<AsterCombinedStream<AsterOrderUpdate>>("ORDER_TRADE_UPDATE", _lk, DoHandleMessage),
                new MessageRoute<AsterCombinedStream<AsterSocketEvent>>("listenKeyExpired", _lk, DoHandleMessage)
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

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, AsterCombinedStream<AsterConfigUpdate> message)
        {
            message.Data.ListenKey = message.Stream;
            _configHandler?.Invoke(
                new DataEvent<AsterConfigUpdate>(message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime)
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, AsterCombinedStream<AsterMarginUpdate> message)
        {
            message.Data.ListenKey = message.Stream;
            _marginHandler?.Invoke(
                new DataEvent<AsterMarginUpdate>(message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime)
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, AsterCombinedStream<AsterAccountUpdate> message)
        {
            message.Data.ListenKey = message.Stream;
            _accountHandler?.Invoke(
                new DataEvent<AsterAccountUpdate>(message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime)
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, AsterCombinedStream<AsterOrderUpdate> message)
        {
            message.Data.ListenKey = message.Stream;
            _orderHandler?.Invoke(
                new DataEvent<AsterOrderUpdate>(message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithSymbol(message.Data.UpdateData.Symbol)
                    .WithDataTimestamp(message.Data.EventTime)
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, AsterCombinedStream<AsterSocketEvent> message)
        {
            _listenkeyHandler?.Invoke(
                new DataEvent<AsterSocketEvent>(message.Data, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithStreamId(message.Stream)
                    .WithDataTimestamp(message.Data.EventTime)
                );
            return CallResult.SuccessResult;
        }
    }
}
