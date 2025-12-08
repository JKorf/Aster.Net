using Aster.Net.Objects.Internal;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
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

        protected override MessageTypeDefinition[] TypeEvaluators { get; } = [

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("e") { Depth = 2 },
                ],
                TypeIdentifierCallback = x => x.FieldValue("e")!,
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
