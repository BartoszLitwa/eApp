using Carter;
using eApp.PlatformService.Api;
using eApp.PlatformService.Api.Constants;
using eApp.PlatformService.Api.Data;
using eApp.PlatformService.Api.Startup;
using eApp.PlatformService.Domain.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateSlimBuilder(args);
builder.WebHost.UseKestrelHttpsConfiguration();

var sqlConfig = builder.Configuration.GetSection(SqlConfig.SqlSection).Get<SqlConfig>()
                ?? throw new ArgumentNullException(SqlConfig.SqlSection);

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    // opt.UseSqlServer(sqlConfig!.ConnectionString);
    opt.UseInMemoryDatabase("InMemory");
});

builder.Services.AddSwagger();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = ApiVersions.V1;
    options.AssumeDefaultVersionWhenUnspecified = true;
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
});

builder.Services.AddAutoMapper(typeof(PlatformProfile).Assembly);

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddCarter();

var app = builder.Build();

app.InitializeApiVersionSet();
app.PrepPopulation();
app.MapCarter();
app.UseHttpsRedirection();

app.AddSwagger();

app.Run();