using Carter;
using eApp.CommandService.Api;
using eApp.CommandService.Api.Data;
using eApp.CommandService.Api.DataServices.Asynchronous;
using eApp.CommandService.Api.DataServices.Synchronous.Grpc;
using eApp.CommandService.Api.EventProcessing;
using eApp.CommandService.Api.Utils;
using eApp.Common.ApiVersioning;
using eApp.Common.Configs;
using eApp.Common.OpenApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppConfig>(builder.Configuration.GetSection(AppConfig.Section));
builder.Services.Configure<ConnectionStringsConfig>(builder.Configuration.GetSection(ConnectionStringsConfig.Section));
builder.Services.Configure<GrpcConfg>(builder.Configuration.GetSection(GrpcConfg.Section));

builder.Services.AddRabbitMqConfig(builder.Configuration);

var connectionStrings = builder.Configuration.GetSection(ConnectionStringsConfig.Section).Get<ConnectionStringsConfig>();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    Console.WriteLine("--> Using MSSQL Server");
    opt.UseSqlServer(connectionStrings.CommandMssql);
});

builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
builder.Services.AddScoped<IPlatformDataClient, PlatformDataClient>();

builder.Services.AddHostedService<MessageBusSubscriber>();

builder.Services.AddSwagger();
builder.Services.AddServiceApiVersioning();

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddAutoMapper(typeof(CommandProfile).Assembly);

builder.Services.AddCarter();

var app = builder.Build();

app.InitializeApiVersionSet();
app.PrepPopulation(app.Environment.IsProduction());
app.MapCarter();
app.AddSwagger();

app.Run();