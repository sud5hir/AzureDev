// See https://aka.ms/new-console-template for more information
using AzureAD;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.Graph;
using Microsoft.Graph.Auth;

Console.WriteLine("Hello, World!");
var app = LoadAppSettings();

var id = app["appId"];
var scope = app["scopes"];

var scopes = scope.Split(';');

var _clientId = "d981af55-1257-44d9-80b7-d8d8caa620a7";
var _tentantId = "b1f35134-ba90-4802-af6b-cf38d86edaa1";

var app1 = PublicClientApplicationBuilder.Create(_clientId)
                                      .WithAuthority(AzureCloudInstance.AzurePublic, _tentantId)
                                      .WithRedirectUri("http://localhost")
                                      .Build();

AuthenticationResult app2 = await app1.AcquireTokenInteractive(scopes).ExecuteAsync();
Console.WriteLine(app2.AccessToken);

var provider = new InteractiveAuthenticationProvider(app1, scopes);
var cli = new GraphServiceClient(provider);

User me = await cli.Me.Request().GetAsync();
Console.WriteLine($"Display Name:\t{me.DisplayName}");


GraphHelper.Initialize(id, scopes, (code, cancellationToken) =>
{
    Console.WriteLine(code.Message);
    return Task.FromResult(0);
});

var accessToken = GraphHelper.GetAccessTokenAsync(scopes).Result;
Console.WriteLine("AccessToek {0}", accessToken);

static IConfigurationRoot LoadAppSettings()
{
    var appConfig = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

    if (string.IsNullOrEmpty(appConfig["appId"]) || string.IsNullOrEmpty(appConfig["scopes"]))
    {
        return null;
    }

    return appConfig;
}

