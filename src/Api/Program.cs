/// Uses examples demonstrated here: 
/// https://youtu.be/CkGFV5bekbY -  Endpoint registration
/// https://www.youtube.com/watch?v=lHC38t1w9Nc - Channels

using Api;
using Api.Features.Books.Commands;
using Api.Features.SomeOtherFeature;
using System.Reflection;
using System.Threading.Channels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());
builder.Services.AddHostedService<SomeProcessor>();

// Channels

builder.Services.AddSingleton<Channel<TestCommand>>(_ => Channel.CreateUnbounded<TestCommand>(new UnboundedChannelOptions
{
	SingleReader = true,
	AllowSynchronousContinuations = false
}));






var app = builder.Build();

app.MapEndpoints();

await app.RunAsync();
