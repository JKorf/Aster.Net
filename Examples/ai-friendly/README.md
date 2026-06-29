# AI-Friendly Examples

These examples are optimized for AI coding assistants and quick onboarding. Each file is:

- Compilable - drop into a console project with `dotnet add package Jkorf.Aster.Net` and it builds
- Self-contained - single file, no external setup, no shared helpers
- Heavily commented - explains why each pattern is used
- Idiomatic - follows current Aster.Net V3 patterns

## Files

| File | What it shows |
|---|---|
| `01-spot-quickstart.cs` | V3 client setup, public ticker, authenticated account balance, place limit order, query order status |
| `02-futures.cs` | Futures V3: set leverage, place market order, get position, close position |
| `03-websocket.cs` | Subscribe to ticker updates and klines with proper teardown |
| `04-multi-exchange.cs` | `CryptoExchange.Net.SharedApis` pattern for exchange-agnostic code |
| `05-error-handling.cs` | `HttpResult` patterns, retry, common error scenarios |

## Running

```bash
dotnet new console -n MyAsterApp
cd MyAsterApp
dotnet add package Jkorf.Aster.Net
# Copy the example .cs file content into Program.cs
# Replace USER_PRIVATE_KEY / SIGNER_PRIVATE_KEY placeholders for private endpoints
dotnet run
```

