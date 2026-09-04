using Aster.Net.Clients.FuturesApi;
using Aster.Net.Enums;
using Aster.Net.Interfaces.Clients.SpotApi;
using Aster.Net.Objects.Models;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Clients.SpotApi
{
    internal partial class AsterRestClientSpotSharedApi
    {
        #region Transfer

        public TransferOptions TransferOptions { get; } = new TransferOptions(_exchangeName, [
            SharedAccountType.Spot,
            SharedAccountType.PerpetualLinearFutures
            ]);
        async Task<ICallResult<SharedId>> ITransfer.TransferAsync(TransferRequest request, CancellationToken ct)
            => await TransferAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedId>> TransferAsync(TransferRequest request, CancellationToken ct)
        {
            var validationError = TransferOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var transferType = GetTransferType(request);
            if (transferType == null)
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid("To/From AccountType", "invalid to/from account combination"));

            // Get data
            var transfer = await _api.Account.TransferAsync(
                request.Asset,
                transferType.Value,
                request.Quantity,
                ct: ct).ConfigureAwait(false);
            if (!transfer.Success)
                return HttpResult.Fail<SharedId>(transfer);

            return HttpResult.Ok(transfer, new SharedId(transfer.Data.TransactionId.ToString()));
        }

        #endregion

        private TransferDirection? GetTransferType(TransferRequest request)
        {
            if (request.FromAccountType == SharedAccountType.Spot && request.ToAccountType.IsFuturesAccount())
                return TransferDirection.SpotToFutures;
            else if (request.FromAccountType.IsFuturesAccount() && request.ToAccountType == SharedAccountType.Spot)
                return TransferDirection.FuturesToSpot;

            return null;
        }

    }
}
