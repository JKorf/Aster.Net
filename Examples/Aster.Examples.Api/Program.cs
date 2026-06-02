using Aster.Net;
using Aster.Net.Interfaces.Clients;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the Aster services
builder.Services.AddAster();

// OR to provide API credentials for accessing private endpoints, or setting other options:
/*
builder.Services.AddAster(options =>
{
    options.ApiCredentials = new AsterCredentials()
        .WithV3("USER_PRIVATE_KEY", "SIGNER_PRIVATE_KEY");
    options.Rest.RequestTimeout = TimeSpan.FromSeconds(5);
});
*/

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// Map the endpoint and inject the rest client
app.MapGet("/{Symbol}", async ([FromServices] IAsterRestClient client, string symbol) =>
{
    var result = await client.SpotV3Api.ExchangeData.GetTickerAsync(symbol);
    return result.Success
        ? Results.Ok(result.Data.LastPrice)
        : Results.Problem(result.Error?.Message, statusCode: 502);
})
.WithOpenApi();


app.MapGet("/Balances", async ([FromServices] IAsterRestClient client) =>
{
    var result = await client.SpotV3Api.Account.GetAccountInfoAsync();
    return result.Success
        ? Results.Ok(result.Data.Balances)
        : Results.Problem(result.Error?.Message, statusCode: 502);
})
.WithOpenApi();

app.Run();
