---
name: aster-net
description: Build C#/.NET integrations using Aster.Net for Aster DEX Spot V3 and Futures V3 REST, WebSockets, authentication, account operations, trading, and Shared APIs. Use when writing or updating code that consumes the library.
---

# Aster.Net

Use `AsterRestClient` and `AsterSocketClient` for Aster exchange access. They provide signing, rate limiting, result handling, and subscription management.

## Setup

```sh
dotnet add package Jkorf.Aster.Net
```

Use the API provided by the installed package. Aster.Net 5.x exposes `SpotV3Api` and `FuturesV3Api`; V1 clients and HMAC credentials were removed.

## Consumer conventions

- Public market data needs no credentials. Private calls use `AsterCredentials.WithV3` with wallet and signer `EthKey` values; see [authentication](references/usage.md#authentication).
- Native REST calls return `HttpResult<T>` or `HttpResult`. WebSocket subscriptions return `WebSocketResult<UpdateSubscription>`. Check `Success` before accessing `Data`.
- Reuse clients, await asynchronous methods, and pass cancellation tokens through the workflow.
- Keep socket callbacks short and unsubscribe through the concrete socket client on shutdown.
- Use exchange info and filters to choose valid symbols, quantities, and prices. Let the library generate client order IDs unless external correlation is needed.
- For portable multi-exchange code, use the relevant surface's `SharedApi` and narrow capability interfaces. Use `IAsterSharedApiClient` for multiple surfaces or dynamic capability lookup.

## Task references

Read only the references needed for the task:

- [API surfaces](references/api-surfaces.md): Choose a client member, discover Shared API capabilities, or locate symbol catalogs.
- [Usage](references/usage.md): Authentication, result handling, trading, subscription lifecycle, and dependency injection examples.
- [Operational considerations](references/safety.md): Key handling, ambiguous order outcomes, and retry decisions for account or trading workflows.
