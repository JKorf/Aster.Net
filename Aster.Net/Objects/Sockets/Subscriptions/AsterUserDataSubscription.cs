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

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<AsterCombinedStream<AsterConfigUpdate>> message)
        {
            message.Data.Data.ListenKey = message.Data.Stream;
            _configHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<AsterCombinedStream<AsterMarginUpdate>> message)
        {
            message.Data.Data.ListenKey = message.Data.Stream;
            _marginHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<AsterCombinedStream<AsterAccountUpdate>> message)
        {
            message.Data.Data.ListenKey = message.Data.Stream;
            _accountHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<AsterCombinedStream<AsterOrderUpdate>> message)
        {
            message.Data.Data.ListenKey = message.Data.Stream;
            _orderHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, message.Data.Data.UpdateData.Symbol, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<AsterCombinedStream<AsterSocketEvent>> message)
        {
            _listenkeyHandler?.Invoke(message.As(message.Data.Data, message.Data.Stream, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.EventTime));
            return CallResult.SuccessResult;
        }
    }
}
