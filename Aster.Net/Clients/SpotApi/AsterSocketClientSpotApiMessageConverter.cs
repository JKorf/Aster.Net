using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Collections.Generic;
using System.Text.Json;

namespace Aster.Net.Clients.SpotApi
{
    internal class AsterSocketClientSpotApiMessageConverter : DynamicJsonConverter
    {
        private static readonly HashSet<string> _userEvents = new HashSet<string>
        {
            "outboundAccountPosition",
            "executionReport",
        };

        public override JsonSerializerOptions Options { get; } = AsterExchange._serializerContext;

        protected override MessageEvaluator[] MessageEvaluators { get; } = [

            new MessageEvaluator {
                Priority = 1,
                Fields = [
                    new PropertyFieldReference("stream"),
                    new PropertyFieldReference("e") { Depth = 2, Constraint = x => _userEvents.Contains(x!) },
                ],
                IdentifyMessageCallback = x => x.FieldValue("stream") + x.FieldValue("e"),
            },

            new MessageEvaluator {
                Priority = 2,
                Fields = [
                    new PropertyFieldReference("stream"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("stream"),
            },

            new MessageEvaluator {
                Priority = 3,
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("id"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("id"),
            }
        ];
    }
}
