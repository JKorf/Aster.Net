# Operational considerations

## Credentials

The wallet key identifies the spending wallet; the signer key authorizes signed requests. Construct both with `EthKey.FromPrivateKey`. Use placeholders in examples and retrieve real keys from environment variables or secret storage. Avoid logging credentials or signed request material.

## Trading and account changes

Example prices and quantities are illustrative. Check exchange filters and account position mode before placing an order. Limit orders can fill immediately; cancellation is not a guarantee against execution.

For futures closes, match the position mode: an opposite-side reduce-only order is appropriate for one-way positions; hedge-mode orders require the applicable `positionSide` and endpoint-supported parameters. Inspect nested results in batch or strategy responses.

## Retry decisions

Use `Error.IsTransient` to identify retry candidates, with bounded backoff and cancellation. A timeout during order submission can leave the outcome unknown. Reconcile the order state before resubmitting; a transient error alone does not make an account-changing request safe to repeat.

See [usage](usage.md) for client and result patterns and [API surfaces](api-surfaces.md) for order lookup methods.
