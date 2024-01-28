using Carter;
using eApp.PlatformService.Api;
using eApp.PlatformService.Api.Constants;
using eApp.PlatformService.Api.Data;
using eApp.PlatformService.Domain.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateSlimBuilder(args);

var sqlConfig = builder.Configuration.GetSection(SqlConfig.SqlSection).Get<SqlConfig>()
                ?? throw new ArgumentNullException(SqlConfig.SqlSection);

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    // opt.UseSqlServer(sqlConfig!.ConnectionString);
    opt.UseInMemoryDatabase("InMemory");
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = ApiVersions.V1;
    options.AssumeDefaultVersionWhenUnspecified = true;
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCarter();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

app.InitializeApiVersionSet();

app.PrepPopulation();

app.MapCarter();

app.Run();