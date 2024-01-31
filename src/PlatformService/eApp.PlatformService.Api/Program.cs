using Carter;
using eApp.Common.ApiVersioning;
using eApp.Common.OpenApi;
using eApp.PlatformService.Api;
using eApp.PlatformService.Api.Data;
using eApp.PlatformService.Api.SyncDataServices.Http;
using eApp.PlatformService.Api.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateSlimBuilder(args);
builder.WebHost.UseKestrelHttpsConfiguration();

builder.Services.Configure<AppConfig>(builder.Configuration.GetSection(AppConfig.Section));

var appConfig = builder.Configuration.GetSection(AppConfig.Section).Get<AppConfig>();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    if (builder.Environment.IsProduction())
    {
        Console.WriteLine("--> Using MSSQL Server");
        opt.UseSqlServer(appConfig.ConnectionStrings.PlatformMssql);
    }
    else
    {
        Console.WriteLine("--> Using InMemory Database");
        opt.UseInMemoryDatabase("InMemory");
    }
});

builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

builder.Services.AddSwagger();
builder.Services.AddServiceApiVersioning();
builder.Services.AddAutoMapper(typeof(PlatformProfile).Assembly);
builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddCarter();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

app.InitializeApiVersionSet();
app.PrepPopulation(app.Environment.IsProduction());
app.MapCarter();
app.AddSwagger();

app.Run();