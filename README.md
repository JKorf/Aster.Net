# ![Aster.Net](https://raw.githubusercontent.com/JKorf/Aster.Net/main/Aster.Net/Icon/icon.png) Aster.Net  

[![.NET](https://img.shields.io/github/actions/workflow/status/JKorf/Aster.Net/dotnet.yml?style=for-the-badge)](https://github.com/JKorf/Aster.Net/actions/workflows/dotnet.yml) ![License](https://img.shields.io/github/license/JKorf/Aster.Net?style=for-the-badge) ![Since](https://img.shields.io/badge/since-2025-brightgreen?style=for-the-badge)

Aster.Net is a client library for accessing the [Aster DEX REST and Websocket API](https://asterdex.github.io/aster-api-website/futures-v3/general-info/). 

## Features
* Response data is mapped to descriptive models
* Input parameters and response values are mapped to discriptive enum values where possible
* High performance
* Automatic websocket (re)connection management 
* Client side rate limiting 
* Client side order book implementation
* Support for managing different accounts
* Extensive logging
* Support for different environments
* Easy integration with other exchange clients based on the CryptoExchange.Net base library
* Native AOT support

## Supported Frameworks
The library is targeting both `.NET Standard 2.0` and `.NET Standard 2.1` for optimal compatibility, as well as the latest dotnet versions to use the latest framework features.

|.NET implementation|Version Support|
|--|--|
|.NET Core|`2.0` and higher|
|.NET Framework|`4.6.1` and higher|
|Mono|`5.4` and higher|
|Xamarin.iOS|`10.14` and higher|
|Xamarin.Android|`8.0` and higher|
|UWP|`10.0.16299` and higher|
|Unity|`2018.1` and higher|

## Install the library

### NuGet 
[![NuGet version](https://img.shields.io/nuget/v/JKorf.Aster.net.svg?style=for-the-badge)](https://www.nuget.org/packages/JKorf.Aster.Net)  [![Nuget downloads](https://img.shields.io/nuget/dt/JKorf.Aster.Net.svg?style=for-the-badge)](https://www.nuget.org/packages/JKorf.Aster.Net)

	dotnet add package JKorf.Aster.Net
	
### GitHub packages
Aster.Net is available on [GitHub packages](https://github.com/JKorf/Aster.Net/pkgs/nuget/JKorf.Aster.Net). You'll need to add `https://nuget.pkg.github.com/JKorf/index.json` as a NuGet package source.

### Download release
[![GitHub Release](https://img.shields.io/github/v/release/JKorf/Aster.Net?style=for-the-badge&label=GitHub)](https://github.com/JKorf/Aster.Net/releases)

The NuGet package files are added along side the source with the latest GitHub release which can found [here](https://github.com/JKorf/Aster.Net/releases).

## How to use
*Basic request:*
```csharp
// Get the ETH/USDT ticker via rest request
var restClient = new AsterRestClient();
var tickerResult = await restClient.SpotV3Api.ExchangeData.GetTickerAsync("ETHUSDT");
var lastPrice = tickerResult.Data.LastPrice;	
```

*Place order:*
```csharp
var restClient = new AsterRestClient(opts => {
	opts.ApiCredentials = new AsterCredentials(new AsterV3Credential("PRIVATEKEY", "SIGNERPRIVATEKEY"));
});

// Place Limit order to go long for 0.1 ETH at 2000
var orderResult = await asterRestClient.FuturesV3Api.Trading.PlaceOrderAsync(
	"ETHUSDT",
	OrderSide.Buy,
	OrderType.Limit,
	0.1m,
	2000,
	positionSide: PositionSide.Long);
```

*WebSocket subscription:*
```csharp
// Subscribe to ETH/USDT ticker updates via the websocket API
var socketClient = new AsterSocketClient();
var tickerSubscriptionResult = socketClient.SpotV3Api.SubscribeToTickerUpdatesAsync("ETHUSDT", (update) => 
{
  var lastPrice = update.Data.LastPrice;
});
```

For information on the clients, dependency injection, response processing and more see the [documentation](https://cryptoexchange.jkorf.dev/client-libs/getting-started), or have a look at the examples [here](https://github.com/JKorf/Aster.Net/tree/main/Examples) or [here](https://github.com/JKorf/CryptoExchange.Net/tree/master/Examples).

**NOTE**  
Aster.Net uses the Builder Code mechanism for Aster when using the V3 API, which means that an additional 1bps / 0.01% fee is charged on top of orders placed with the library to fund development. This is entirely optional and can be disabled in the client options by setting `BuilderFeePercentage` to `0` or `null` in the client options.

## AI documentation
For AI coding assistants and quick onboarding:
* [AGENTS.md](AGENTS.md) skill-style instructions for Aster.Net usage
* [`llms.txt`](llms.txt) provides concise AI context
* [`llms-full.txt`](llms-full.txt) provides detailed patterns, pitfalls and endpoint routing
* [`docs/ai-api-map.md`](docs/ai-api-map.md) maps common intents to V3 client members
* [`Examples/ai-friendly`](Examples/ai-friendly) contains compact examples that are compiled by the test suite

See [cryptoexchange-skills-hub](https://github.com/JKorf/cryptoexchange-skills-hub) for installable skills.

## Authentication
Depending on the API used different credentials types are required.  
**V3 | SpotV3Api / FuturesV3Api**  
The V3 API is recommended, API credentials can be provided via the AsterV3Credential overload available on the AsterCredentials object.  
There are 2 ways to provide credentials for the V3 API:
1. Use the private key of your wallet connected to Aster and the private signer key, which can be obtained via the Aster UI under [Api Management] => [Pro Api]  
This gives access to all endpoints.
```
var credentials = new AsterCredentials(new AsterV3Credential("PRIVATEKEY", "SIGNERPRIVATEKEY"));
var client = new AsterRestClient(opts => { opts.ApiCredentials = credentials; });
var result = await client.FuturesV3Api.Account.GetBalancesAsync();
```

2. Use the public address of your wallet connected to Aster and the public and private signer key which can be obtained via the Aster UI under [Api Management] => [Pro Api]  
This gives access to most but not all endpoints.
```
var credentials = new AsterCredentials(new AsterV3Credential("PUBLICADDRESS", "SIGNERPUBLICKEY", "SIGNERPRIVATEKEY"));
var client = new AsterRestClient(opts => { opts.ApiCredentials = credentials; });
var result = await client.FuturesV3Api.Account.GetBalancesAsync();
```

**V1 | SpotApi / FuturesApi**  
The V1 API only requires the credentials which can be obtained via the Aster UI under [Api Management] => [Api]
```
var credentials = new AsterCredentials(new HMACCredential("APIKEY", "APISECRET"));
var client = new AsterRestClient(opts => { opts.ApiCredentials = credentials; });
var result = await client.FuturesApi.Account.GetBalancesAsync();
```

## CryptoExchange.Net
Aster.Net is based on the [CryptoExchange.Net](https://github.com/JKorf/CryptoExchange.Net) base library. Other exchange API implementations based on the CryptoExchange.Net base library are available and follow the same logic.

CryptoExchange.Net also allows for [easy access to different exchange API's](https://jkorf.github.io/CryptoExchange.Net#idocs_shared).

|Exchange|Repository|Nuget|
|--|--|--|
|Binance|[JKorf/Binance.Net](https://github.com/JKorf/Binance.Net)|[![Nuget version](https://img.shields.io/nuget/v/Binance.net.svg?style=flat-square)](https://www.nuget.org/packages/Binance.Net)|
|BingX|[JKorf/BingX.Net](https://github.com/JKorf/BingX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.BingX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.BingX.Net)|
|Bitfinex|[JKorf/Bitfinex.Net](https://github.com/JKorf/Bitfinex.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bitfinex.net.svg?style=flat-square)](https://www.nuget.org/packages/Bitfinex.Net)|
|Bitget|[JKorf/Bitget.Net](https://github.com/JKorf/Bitget.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Bitget.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Bitget.Net)|
|BitMart|[JKorf/BitMart.Net](https://github.com/JKorf/BitMart.Net)|[![Nuget version](https://img.shields.io/nuget/v/BitMart.net.svg?style=flat-square)](https://www.nuget.org/packages/BitMart.Net)|
|BitMEX|[JKorf/BitMEX.Net](https://github.com/JKorf/BitMEX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.BitMEX.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.BitMEX.Net)|
|Bitstamp|[JKorf/Bitstamp.Net](https://github.com/JKorf/Bitstamp.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bitstamp.Net.svg?style=flat-square)](https://www.nuget.org/packages/Bitstamp.Net)|
|BloFin|[JKorf/BloFin.Net](https://github.com/JKorf/BloFin.Net)|[![Nuget version](https://img.shields.io/nuget/v/BloFin.net.svg?style=flat-square)](https://www.nuget.org/packages/BloFin.Net)|
|Bybit|[JKorf/Bybit.Net](https://github.com/JKorf/Bybit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bybit.net.svg?style=flat-square)](https://www.nuget.org/packages/Bybit.Net)|
|Coinbase|[JKorf/Coinbase.Net](https://github.com/JKorf/Coinbase.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Coinbase.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Coinbase.Net)|
|CoinEx|[JKorf/CoinEx.Net](https://github.com/JKorf/CoinEx.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinEx.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinEx.Net)|
|CoinGecko|[JKorf/CoinGecko.Net](https://github.com/JKorf/CoinGecko.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinGecko.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinGecko.Net)|
|CoinW|[JKorf/CoinW.Net](https://github.com/JKorf/CoinW.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinW.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinW.Net)|
|Crypto.com|[JKorf/CryptoCom.Net](https://github.com/JKorf/CryptoCom.Net)|[![Nuget version](https://img.shields.io/nuget/v/CryptoCom.net.svg?style=flat-square)](https://www.nuget.org/packages/CryptoCom.Net)|
|DeepCoin|[JKorf/DeepCoin.Net](https://github.com/JKorf/DeepCoin.Net)|[![Nuget version](https://img.shields.io/nuget/v/DeepCoin.net.svg?style=flat-square)](https://www.nuget.org/packages/DeepCoin.Net)|
|Gate.io|[JKorf/GateIo.Net](https://github.com/JKorf/GateIo.Net)|[![Nuget version](https://img.shields.io/nuget/v/GateIo.net.svg?style=flat-square)](https://www.nuget.org/packages/GateIo.Net)|
|HTX|[JKorf/HTX.Net](https://github.com/JKorf/HTX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.HTX.net.svg?style=flat-square)](https://www.nuget.org/packages/Jkorf.HTX.Net)|
|HyperLiquid|[JKorf/HyperLiquid.Net](https://github.com/JKorf/HyperLiquid.Net)|[![Nuget version](https://img.shields.io/nuget/v/HyperLiquid.Net.svg?style=flat-square)](https://www.nuget.org/packages/HyperLiquid.Net)|
|Kraken|[JKorf/Kraken.Net](https://github.com/JKorf/Kraken.Net)|[![Nuget version](https://img.shields.io/nuget/v/KrakenExchange.net.svg?style=flat-square)](https://www.nuget.org/packages/KrakenExchange.Net)|
|Kucoin|[JKorf/Kucoin.Net](https://github.com/JKorf/Kucoin.Net)|[![Nuget version](https://img.shields.io/nuget/v/Kucoin.net.svg?style=flat-square)](https://www.nuget.org/packages/Kucoin.Net)|
|Lighter|[JKorf/Lighter.Net](https://github.com/JKorf/Lighter.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Lighter.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Lighter.Net)|
|Mexc|[JKorf/Mexc.Net](https://github.com/JKorf/Mexc.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Mexc.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Mexc.Net)|
|OKX|[JKorf/OKX.Net](https://github.com/JKorf/OKX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.OKX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.OKX.Net)|
|Polymarket|[JKorf/Polymarket.Net](https://github.com/JKorf/Polymarket.Net)|[![Nuget version](https://img.shields.io/nuget/v/Polymarket.net.svg?style=flat-square)](https://www.nuget.org/packages/Polymarket.Net)|
|Toobit|[JKorf/Toobit.Net](https://github.com/JKorf/Toobit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Toobit.net.svg?style=flat-square)](https://www.nuget.org/packages/Toobit.Net)|
|Upbit|[JKorf/Upbit.Net](https://github.com/JKorf/Upbit.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Upbit.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Upbit.Net)|
|Weex|[JKorf/Weex.Net](https://github.com/JKorf/Weex.Net)|[![Nuget version](https://img.shields.io/nuget/v/Weex.net.svg?style=flat-square)](https://www.nuget.org/packages/Weex.Net)|
|WhiteBit|[JKorf/WhiteBit.Net](https://github.com/JKorf/WhiteBit.Net)|[![Nuget version](https://img.shields.io/nuget/v/WhiteBit.net.svg?style=flat-square)](https://www.nuget.org/packages/WhiteBit.Net)|
|XT|[JKorf/XT.Net](https://github.com/JKorf/XT.Net)|[![Nuget version](https://img.shields.io/nuget/v/XT.net.svg?style=flat-square)](https://www.nuget.org/packages/XT.Net)|

When using multiple of these API's the [CryptoClients.Net](https://github.com/JKorf/CryptoClients.Net) package can be used which combines this and the other packages and allows easy access to all exchange API's.

## Discord
[![Nuget version](https://img.shields.io/discord/847020490588422145?style=for-the-badge)](https://discord.gg/MSpeEtSY8t)  
A Discord server is available [here](https://discord.gg/MSpeEtSY8t). For discussion and/or questions around the CryptoExchange.Net and implementation libraries, feel free to join.

## Supported functionality

### Spot V1 REST
|API|Supported|Location|
|--|--:|--|
|Market Data|✓|`restClient.SpotApi.ExchangeData`|
|Account and Trading|✓|`restClient.SpotApi.Account` / `restClient.SpotApi.Trading`|

### Spot V1 WebSocket
|API|Supported|Location|
|--|--:|--|
|Market Data|✓|`socketClient.SpotApi`|
|Account Information|✓|`socketClient.SpotApi`|

### Spot V3 REST
|API|Supported|Location|
|--|--:|--|
|Market Data|✓|`restClient.SpotV3Api.ExchangeData`|
|Account and Trading|✓|`restClient.SpotV3Api.Account` / `restClient.SpotV3Api.Trading`|

### Spot V3 WebSocket
|API|Supported|Location|
|--|--:|--|
|Market Data|✓|`socketClient.SpotV3Api`|
|Account Information|✓|`socketClient.SpotV3Api`|

### Futures REST
|API|Supported|Location|
|--|--:|--|
|Market Data|✓|`restClient.FuturesApi.ExchangeData`|
|Account and Trading|✓|`restClient.FuturesApi.Account` / `restClient.FuturesV3Api.Trading`|

### Futures WebSocket
|API|Supported|Location|
|--|--:|--|
|Market Data|✓|`socketClient.FuturesApi`|
|Account Information|✓|`socketClient.FuturesApi`|

### Futures V3 REST
|API|Supported|Location|
|--|--:|--|
|Market Data|✓|`restClient.FuturesV3Api.ExchangeData`|
|Account and Trading|✓|`restClient.FuturesV3Api.Account` / `restClient.FuturesV3Api.Trading`|

### Futures V3 WebSocket
|API|Supported|Location|
|--|--:|--|
|Market Data|✓|`socketClient.FuturesV3Api`|
|Account Information|✓|`socketClient.FuturesV3Api`|

## Support the project
Any support is greatly appreciated.

### Donate
Make a one time donation in a crypto currency of your choice. If you prefer to donate a currency not listed here please contact me.

**Btc**:  bc1q277a5n54s2l2mzlu778ef7lpkwhjhyvghuv8qf  
**Eth**:  0xcb1b63aCF9fef2755eBf4a0506250074496Ad5b7   
**USDT (TRX)**  TKigKeJPXZYyMVDgMyXxMf17MWYia92Rjd 

### Sponsor
Alternatively, sponsor me on Github using [Github Sponsors](https://github.com/sponsors/JKorf). 

## Release notes
* Version 4.1.0 - 09 Jul 2026
    * Updated CryptoExchange.Net to v12.1.0Added strategy order endpoints
    * Added Pending order status
    * Added MarginType.Cross value mapping
    * Fixed some ChaseOrder endpoint issues

* Version 4.0.0 - 29 Jun 2026
    * Result types:
      * (Web)CallResult types are replaced by HttpResult, WebSocketResult and QueryResult with the same logic
      * WebSocketResult and QueryResult now return additional info for websocket operations
      * Updated result types to record type
      * Removed implicit result type conversion to bool, `if (result)` no longer works, instead use `if (result.Success)`
      * Fixed result object nullability hinting, for example Data might be null if Success isn't checked for true
    * Clients:
      * Added ToString overrides on base API types
      * Added Exchange property on BaseApiClient
      * Added ApiCredentials property on Api clients
      * Updated ILogger source from client name to topic specific client name
      * Removed logging from client creation
      * Fixed issue in SocketApiClient.GetSocketConnection causing requests to always wait the full max 10 seconds when there was a reconnecting socket
    * Shared APIs:
      * Added missing dedicated option types
      * Added Discover method on ISharedClient interface, returning info on supported capabilities and operations
      * Added ResetStaticExchangeParameters method on ExchangeParameters
      * Added Status property to SharedWithdrawal model
      * Added TradingModes property to SharedBalance model
      * Updated Shared ExchangeParameters parameter names to be case insensitive
      * Updated code comments
      * Replaced ExchangeResult with ExchangeCallResult type
      * Removed TradingMode from the response model, only maintained on models where it makes sense
      * Removed IListenKey support, listen keys now rely on internal management
    * Added user subscription overloads without listenkey which manages the listen key internally
    * Added async streaming on UserDataTracker items with StreamUpdatesAsync
    * Added cancellation token support to UserDataTracker starting
    * Added SupportedEnvironments property to PlatformInfo
    * Added Clear() method on UserClientProvider to clear all cached clients
    * Added setter to AsterExchange.RateLimiter to allow custom rate limit settings
    * Updated FuturesV3 EditOrderAsync parameters to non-nullable
    * Various small performance improvements
    * Fixed websocket connection attempts counting towards rate limit even when server could not be reached
