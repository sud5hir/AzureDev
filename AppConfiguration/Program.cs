// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;

Console.WriteLine("Hello, World!");
var builder = new ConfigurationBuilder();
builder.AddAzureAppConfiguration(Environment.GetEnvironmentVariable("ConnectionString"));

var config = builder.Build();

Console.WriteLine(config["connstring"] ?? "Hello world!");