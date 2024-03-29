﻿using Carter;
using eApp.Common.ApiVersioning;
using eApp.PlatformService.Api.Dtos.Platform;
using eApp.PlatformService.Api.Platforms.Commands;
using eApp.PlatformService.Api.Platforms.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eApp.PlatformService.Api.Platforms;

public class PlatformsModule() : CarterModule("/api/platforms")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("");

        group.MapGet("/", async (ISender sender) =>
        {
            var result = await sender.Send(new GetAllPlatformsQuery());
            return result.IsFailure ? Results.BadRequest(result.Error) : Results.Ok(result.Value);
        }).WithApiVersionSet(ApiVersions.ApiVersionSet)
            .MapToApiVersion(ApiVersions.V1);
        
        group.MapGet("/{id:required}", async ([FromQuery] int id, ISender sender) =>
            {
                var result = await sender.Send(new GetPlatformByIdQuery(id));
                return result.IsFailure ? Results.NotFound(result.Error) : Results.Ok(result.Value);
            }).WithApiVersionSet(ApiVersions.ApiVersionSet)
            .MapToApiVersion(ApiVersions.V1);

        group.MapPost("/", async ([FromBody] PlatformCreateDto dto, ISender sender) =>
        {
            var result = await sender.Send(new CreatePlatformCommand(dto.Name, dto.Publisher, dto.Cost));
            return result.IsFailure ? Results.BadRequest(result.Error) : Results.Ok(result.Value);
        }).WithApiVersionSet(ApiVersions.ApiVersionSet)
            .MapToApiVersion(ApiVersions.V1);
    }
}