# ![Aster.Net](https://raw.githubusercontent.com/JKorf/Aster.Net/main/Aster.Net/Icon/icon.png) Aster.Net  

[![.NET](https://img.shields.io/github/actions/workflow/status/JKorf/Aster.Net/dotnet.yml?style=for-the-badge)](https://github.com/JKorf/Aster.Net/actions/workflows/dotnet.yml) ![License](https://img.shields.io/github/license/JKorf/Aster.Net?style=for-the-badge)

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
var tickerResult = await restClient.SpotApiV3.ExchangeData.GetTickerAsync("ETHUSDT");
var lastPrice = tickerResult.Data.LastPrice;	
```

*Place order:*
```csharp
var restClient = new AsterRestClient(opts => {
	opts.ApiCredentials = new AsterV3Credential("PRIVATEKEY", "SIGNERPRIVATEKEY");
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
var tickerSubscriptionResult = socketClient.SpotApiV3.SubscribeToTickerUpdatesAsync("ETHUSDT", (update) => 
{
  var lastPrice = update.Data.LastPrice;
});
```

For information on the clients, dependency injection, response processing and more see the [documentation](https://cryptoexchange.jkorf.dev/client-libs/getting-started), or have a look at the examples [here](https://github.com/JKorf/Aster.Net/tree/main/Examples) or [here](https://github.com/JKorf/CryptoExchange.Net/tree/master/Examples).

**NOTE**  
Aster.Net uses the Builder Code mechanism for Aster when using the V3 API, which means that an additional 1bps / 0.01% fee is charged on top of orders placed with the library to fund development. This is entirely optional and can be disabled in the client options by setting `BuilderFeePercentage` to `0` or `null` in the client options.

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
* Version 3.2.1 - 24 Apr 2026
    * Fixed restClient.FuturesV3Api.Account.ModifyIsolatedMarginAsync parameter serialization

* Version 3.2.0 - 12 Apr 2026
    * Added Agent endpoints
    * Added AsterV3Credentials overload for using public address
    * Added additional checks for builder parameters
    * Fix for REST error message parsing
    * Removed BuilderName option and parameter from ApproveBuilderAsync endpoint

* Version 3.1.0 - 09 Apr 2026
    * Updated CryptoExchange.Net to version 11.1.0, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes
    * Updated restClient.FuturesV3Api.Account.GetAccountInfoAsync path from /account to /accountWithJoinMargin
    * Added client.FuturesV3Api.Account.GetWithdrawInfoAsync endpoint
    * Added client.FuturesV3Api.Account.GetDepositWithdrawHistoryAsync endpoint

* Version 3.0.1 - 02 Apr 2026
    * Updated CryptoExchange.Net to version 11.0.3, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes
    * Fixed FuturesV3 UserStream endpoints

* Version 3.0.0 - 24 Mar 2026
    * Updated CryptoExchange.Net to version 11.0.1, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes
    * Updated class for supplying API credentials from ApiCredentials to AsterCredentials
    * Added FuturesV3 and SpotV3 API's
    * Added 1bps builder fee enabled by default on V3 API, can be disabled in the options
    * Updated Shared order status parsing to default to Unknown value if not parsable
    * Added LastUpdateId property to AsterBookTicker model
    * Added Notional and IsolatedWallet properties to AsterPosition model
    * Added OrderListId property to AsterSpotOrder model
    * Added BaseAssetAddress and ListingTime properties to AsterSpotSymbol model
    * Added BaseAsset and QuoteAsset properties to AsterSpotTicker model
    * Added CounterpartyId property to AsterSpotUserTrade model
    * Added TwapMinNotional and CreateTime properties to AsterSymbol model
    * Added MarginAsset property to AsterUserTrade model
    * Added Testnet environment for the V3 API

    * Notes for updating:
        * Update ApiCredentials to AsterCredentials for authentication, i.e. `ApiCredentials = new ApiCredentials(..)` => `ApiCredentials = new AsterCredentials(..)`

* Version 2.8.0 - 06 Mar 2026
    * Updated CryptoExchange.Net to version 10.8.0, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes
    * Improved method XML comments

* Version 2.7.0 - 24 Feb 2026
    * Updated CryptoExchange.Net to version 10.7.0
    * Added additional Http settings to client options
    * Updated Shared REST interfaces pagination logic
    * Updated HttpClient registration, fixing issue of DNS changes not getting processed
    * Fixed UserClientProvider using unconfigured HttpClient

* Version 2.6.0 - 16 Feb 2026
    * Updated CryptoExchange.Net to version 10.6.0, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes
    * Fixed SymbolOrderBook websocket subscription not getting closed if when waiting for initial data times out

* Version 2.5.0 - 10 Feb 2026
    * Updated CryptoExchange.Net to version 10.5.1, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes
    * Updated UserClientProvider internal client cache to non-static to prevent cleanup issues

* Version 2.4.0 - 06 Feb 2026
    * Updated CryptoExchange.Net to version 10.4.0, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes
    * Added AsterUserSpotDataTracker and AsterUserFuturesDataTracker
    * Added additional methods for requesting supported symbols to Shared ISpotSymbolRestClient/IFuturesSymbolRestClient interfaces
    * Added PositionMode mapping on SharedPosition models
    * Fixed disposed clients getting returned from UserClientProvider

* Version 2.3.0 - 22 Jan 2026
    * Updated CryptoExchange.Net to version 10.3.0, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes
    * Removed legacy websocket message handling and the corresponding UseUpdatedDeserialization client option
    * Added Metadata to AsterExchange

* Version 2.2.2 - 19 Jan 2026
    * Updated CryptoExchange.Net to version 10.2.5, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes
    * Added Delisted value to SymbolStatus enum
    * Fixed some order book sync issues

* Version 2.2.1 - 14 Jan 2026
    * Updated CryptoExchange.Net to version 10.2.3, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes

* Version 2.2.0 - 13 Jan 2026
    * Updated CryptoExchange.Net to version 10.2.0, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes
    * Added SequenceNumber to order book websocket updates
    * Updated SymbolOrderBook implementations to correctly check sequence numbers

* Version 2.1.0 - 07 Jan 2026
    * Updated CryptoExchange.Net version to 10.1.0, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes
    * Added DataTimeLocal and DataAge properties to DataEvent object
    * Added UpdateServerTime, UpdateLocalTime and DataAge properties to (I)SymbolOrderBook
    * Added missing Spot references on IAsterOrderBookFactory

* Version 2.0.0 - 16 Dec 2025
    * Added Net10.0 target framework
    * Updated CryptoExchange.Net version to 10.0.0, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes
    * Improved performance across the board, biggest gains in websocket message processing
    * Updated REST message response handling
    * Updated WebSocket message handling
    * Added UseUpdatedDeserialization socket client options to toggle by new and old message handling
    * Added various Performance websocket subscriptions for public streams
    * Added SocketIndividualSubscriptionCombineTarget socket client option
    * Updated Shared API's subscription update types from ExchangeEvent to DataEvent
    * Fixed bug in Shared GetFuturesTickersAsync endpoint

* Version 1.3.0 - 11 Nov 2025
    * Updated CryptoExchange.Net version to 9.13.0, see https://github.com/JKorf/CryptoExchange.Net/releases/

* Version 1.2.0 - 03 Nov 2025
    * Updated CryptoExchange.Net to version 9.12.0
    * Added support for using SharedSymbol.UsdOrStable in Shared APIs
    * Fixed deserialization issue in restClient.FuturesApi.ExchangeData.GetFundingInfoAsync
    * Fixed exception when initial trade snapshot has no items in TradeTracker
    * Removed some unhelpful verbose logs

* Version 1.1.0 - 16 Oct 2025
    * Updated CryptoExchange.Net version to 9.10.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added ClientOrderId mapping on SharedUserTrade models
    * Added ITransferRestClient.TransferAsync implementation
    * Updated SpotApi IBalanceRestClient.GetBalancesAsync to support funding wallet balance retrieval
    * Fixed FuturesApi IBalanceRestClient.GetBalancesAsync available balance mapping

* Version 1.0.0 - 06 Oct 2025
    * Initial release

