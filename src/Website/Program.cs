using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Website.Components;
using Website.ConsumptionDays;
using Website.Handlers;
using Website.Hubs;
using Wolverine;
using Wolverine.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
       new[] { "application/octet-stream" });
});


// database
builder.Services.AddDbContext<ConsumptionDaysDbContext>(options =>
{
    options.UseSqlite("Data Source=sqlite.db");
});


// Wolverine
// Wolverine
builder.Host.UseWolverine(opts =>
{
    //opts.PersistMessagesWithSqlServer(connectionString);

    opts.Policies.UseDurableLocalQueues();


    //opts.PublishMessage<ConsumptionDayCreatedEvent>()
    //	.ToRabbitExchange("consumption-exc", exchange =>
    //	{
    //		exchange.ExchangeType = ExchangeType.Direct;
    //		exchange.BindQueue("consumption-queue", "cons2exch");
    //	});

    //opts.PublishMessage<ConsumptionDayCreatedEvent>()
    //    .ToRabbitQueue("consumption-created")
    //    .UseDurableOutbox();


    opts.ListenToRabbitQueue("consumption-created");

    opts.UseRabbitMq(c =>
    {
        c.HostName = "192.168.1.250";
        c.UserName = "guest";
        c.Password = "guest";
        c.VirtualHost = "/";
    })
    .AutoProvision();

    Console.WriteLine(opts.DescribeHandlerMatch(typeof(ConsumptionDayCreatedHandler)));
});



var app = builder.Build();

app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapHub<ConsumptionStreamHub>("/consumptionstreamhub");

app.Run();
