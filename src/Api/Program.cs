/// Uses examples demonstrated here: 
/// https://youtu.be/CkGFV5bekbY -  Endpoint registration
/// https://www.youtube.com/watch?v=lHC38t1w9Nc - Channels

using Api;
using Api.Features.Books.Commands;
using Api.Features.ConsumptionDays;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SharedMessages;
using System;
using System.Reflection;
using System.Threading.Channels;
using Wolverine;
using Wolverine.RabbitMQ;
using Wolverine.SqlServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());
//builder.Services.AddHostedService<SomeProcessor>();

// Channels
builder.Services.AddSingleton<Channel<TestCommand>>(_ => Channel.CreateUnbounded<TestCommand>(new UnboundedChannelOptions
{
	SingleReader = true,
	AllowSynchronousContinuations = false
}));


//string connectionString = "Data Source=:memory:";
string connectionString = "Data Source=sqlite.db";

// In-Memory Database
builder.Services.AddDbContext<ConsumptionDaysDbContext>(options =>
{
	//options.UseSqlite("DataSource=:memory:");
	options.UseSqlite(connectionString);
});


// Wolverine
builder.Host.UseWolverine(opts =>
{
	//opts.PersistMessagesWithSqlServer(connectionString);

    //opts.Policies.UseDurableLocalQueues();


	//opts.PublishMessage<ConsumptionDayCreatedEvent>()
	//	.ToRabbitExchange("consumption-exc", exchange =>
	//	{
	//		exchange.ExchangeType = ExchangeType.Direct;
	//		exchange.BindQueue("consumption-queue", "cons2exch");
	//	});

	opts.PublishMessage<ConsumptionDayCreatedEvent>()
		.ToRabbitQueue("consumption-created")
		.UseDurableOutbox();

	opts.UseRabbitMq(c =>
	{
		c.HostName = "192.168.1.250";
		c.UserName = "guest";
		c.Password = "guest";
		c.VirtualHost = "/";
	})
	.AutoProvision();
});












var app = builder.Build();

using (var Scope = app.Services.CreateScope())
{
    var context = Scope.ServiceProvider.GetRequiredService<ConsumptionDaysDbContext>();
	//context.Database.EnsureCreated();
    context.Database.Migrate();
}



app.MapEndpoints();

await app.RunAsync();
