using Aster.Net.Objects.Internal;
using Aster.Net.Objects.Models;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Collections.Generic;
using System.Text.Json;

namespace Aster.Net.Clients.MessageHandlers
{
    internal class AsterSocketSpotMessageConverter : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = AsterExchange._serializerContext;

        public AsterSocketSpotMessageConverter()
        {
            AddTopicMapping<AsterCombinedStream>(x => x.Stream);
        }

        protected override MessageEvaluator[] TypeEvaluators { get; } = [

            new MessageEvaluator {
                Priority = 1,
                Fields = [
                    new PropertyFieldReference("e") { Depth = 2 },
                ],
                IdentifyMessageCallback = x => x.FieldValue("e")!,
            },

            new MessageEvaluator {
                Priority = 3,
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("id"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("id")!,
            }
        ];

    }
}
