using Carter;
using eApp.Common.ApiVersioning;
using eApp.Common.Configs;
using eApp.Common.OpenApi;
using eApp.PlatformService.Api;
using eApp.PlatformService.Api.Data;
using eApp.PlatformService.Api.DataServices.Asynchronous;
using eApp.PlatformService.Api.DataServices.Synchronous.Grpc;
using eApp.PlatformService.Api.SyncDataServices.Http;
using eApp.PlatformService.Api.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateSlimBuilder(args);
builder.WebHost.UseKestrelHttpsConfiguration();

builder.Services.Configure<AppConfig>(builder.Configuration.GetSection(AppConfig.Section));
builder.Services.Configure<ConnectionStringsConfig>(builder.Configuration.GetSection(ConnectionStringsConfig.Section));
builder.Services.AddRabbitMqConfig(builder.Configuration);

var connectionStrings = builder.Configuration.GetSection(ConnectionStringsConfig.Section).Get<ConnectionStringsConfig>();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    Console.WriteLine("--> Using MSSQL Server");
    opt.UseSqlServer(connectionStrings.PlatformMssql);
});

builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
builder.Services.AddGrpc();

builder.Services.AddServiceApiVersioning();
builder.Services.AddAutoMapper(typeof(PlatformProfile).Assembly);
builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddCarter();
builder.Services.AddSwagger();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

app.MapGrpcService<GrpcPlatformService>();

app.InitializeApiVersionSet();
app.PrepPopulation(app.Environment.IsProduction());
app.MapCarter();
app.AddSwagger();


app.Run();