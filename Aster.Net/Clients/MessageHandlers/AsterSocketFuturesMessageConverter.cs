using Aster.Net.Objects.Internal;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using System.Collections.Generic;
using System.Text.Json;

namespace Aster.Net.Clients.MessageHandlers
{
    internal class AsterSocketFuturesMessageConverter : JsonSocketMessageHandler
    {
        private static readonly HashSet<string?> _userEvents = new HashSet<string?>
        {
            "ACCOUNT_CONFIG_UPDATE",
            "MARGIN_CALL",
            "ACCOUNT_UPDATE",
            "ORDER_TRADE_UPDATE",
            "listenKeyExpired",
        };

        public override JsonSerializerOptions Options { get; } = AsterExchange._serializerContext;

        protected override MessageTypeDefinition[] TypeEvaluators { get; } = [

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("e") { Depth = 2 }.WithFilterConstraint(_userEvents),
                ],
                TypeIdentifierCallback = x => x.FieldValue("e")!,
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("stream"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("stream")!,
            },

            new MessageTypeDefinition {
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("id"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("id")!,
            }
        ];
    }
}
