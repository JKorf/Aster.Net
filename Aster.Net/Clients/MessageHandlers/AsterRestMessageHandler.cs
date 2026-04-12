using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using System.IO;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Aster.Net.Clients.MessageHandlers
{
    internal class AsterRestMessageHandler : JsonRestMessageHandler
    {
        private readonly ErrorMapping _errorMapping;

        public override JsonSerializerOptions Options { get; } = AsterExchange._serializerContext;

        public AsterRestMessageHandler(ErrorMapping errorMapping)
        {
            _errorMapping = errorMapping;
        }

        public override async ValueTask<Error> ParseErrorResponse(
            int httpStatusCode,
            HttpResponseHeaders responseHeaders,
            Stream responseStream)
        {
            var (error, document) = await GetJsonDocument(responseStream).ConfigureAwait(false);
            if (error != null)
                return error;

            int? code = document!.RootElement.TryGetProperty("code", out var codeProp) ? codeProp.GetInt32() : null;
            string? msg = document.RootElement.TryGetProperty("msg", out var msgProp) ? msgProp.GetString() : null;
            string? errorMsg = document.RootElement.TryGetProperty("error", out var errorProp) ? errorProp.GetString() : null;
            if (msg != null && code != null)
            {
                var errorInfo = _errorMapping.GetErrorInfo(code.ToString()!, msg);
                return new ServerError(code.Value.ToString(), errorInfo);
            }

            if (msg != null)
                return new ServerError(ErrorInfo.Unknown with { Message = msg });

            if (errorMsg != null)
                return new ServerError(ErrorInfo.Unknown with { Message = errorMsg });

            return new ServerError(ErrorInfo.Unknown);
        }
    }
}
