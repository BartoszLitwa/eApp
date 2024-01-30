using Carter;
using eApp.Common.ApiVersioning;
using eApp.Common.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwagger();
builder.Services.AddServiceApiVersioning();

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());

builder.Services.AddCarter();

var app = builder.Build();

app.InitializeApiVersionSet();
app.MapCarter();
app.AddSwagger();

app.Run();