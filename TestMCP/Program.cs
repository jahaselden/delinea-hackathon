﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

using TestMCP;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddConsole(consoleLogOptions =>
{
    // Configure all logs to go to stderr
    consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();
builder.Services.AddSingleton<MonkeyService>();
builder.Services.AddSingleton<GitHubService>();
await builder.Build().RunAsync();

// [McpServerToolType] //tool types that are exposed to mcp - three currently
// public static class EchoTool
// {
//     [McpServerTool, Description("Echoes the message back to the client.")]
//     public static string Echo(string message) => $"hello {message}";

//     [McpServerTool, Description("Echoes back the message in reverse.")]
//     public static string Reverse(string message) => new string(message.Reverse().ToArray());

//     [McpServerTool, Description("Echoes back the length of the message.")]
//     public static int Length(string message) => message.Length;
// }