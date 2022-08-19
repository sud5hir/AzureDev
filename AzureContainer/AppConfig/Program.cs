using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

var builder = new ConfigurationBuilder();
builder.AddAzureAppConfiguration(Environment.GetEnvironmentVariable("ConnectionString"));

var config = builder.Build();
Console.WriteLine(config["Message"] ?? "Hello world!");
