using Aster.Net.Enums;
using Aster.Net.Interfaces.Clients.FuturesV3Api;
using Aster.Net.Objects.Models;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Clients.FuturesV3Api
{
    internal partial class AsterRestClientFuturesV3SharedApi
    {
        #region Get Book Ticker

        public GetBookTickerOptions GetBookTickerOptions { get; }
            = new GetBookTickerOptions(_exchangeName, false);
        async Task<ICallResult<SharedBookTicker>> IGetBookTicker.GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
            => await GetBookTickerAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedBookTicker>> GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
        {
            var validationError = GetBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBookTicker>(Exchange, validationError);

            var result = await _api.ExchangeData.GetBookTickerAsync(request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedBookTicker>(result);

            return HttpResult.Ok(result, new SharedBookTicker(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, result.Data.Symbol),
                result.Data.Symbol,
                result.Data.BestAskPrice,
                new SharedOrderQuantity(result.Data.BestAskQuantity),
                result.Data.BestBidPrice,
                new SharedOrderQuantity(result.Data.BestBidQuantity)));
        }

        #endregion

    }
}
