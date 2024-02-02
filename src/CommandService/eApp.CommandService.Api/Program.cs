using Carter;
using eApp.CommandService.Api;
using eApp.CommandService.Api.Data;
using eApp.CommandService.Api.Utils;
using eApp.Common.ApiVersioning;
using eApp.Common.OpenApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppConfig>(builder.Configuration.GetSection(AppConfig.Section));

var appConfig = builder.Configuration.GetSection(AppConfig.Section).Get<AppConfig>();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseInMemoryDatabase("InMem");
});

builder.Services.AddSwagger();
builder.Services.AddServiceApiVersioning();

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddAutoMapper(typeof(CommandProfile).Assembly);

builder.Services.AddCarter();

var app = builder.Build();

app.InitializeApiVersionSet();
app.MapCarter();
app.AddSwagger();

app.Run();