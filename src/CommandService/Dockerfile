FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://0.0.0.0:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ./eApp.CommandService.Api/eApp.CommandService.Api.csproj ./eApp.CommandService.Api/
COPY ./eApp.CommandService.Domain/eApp.CommandService.Domain.csproj ./eApp.CommandService.Domain/
RUN dotnet restore "eApp.CommandService.Api/eApp.CommandService.Api.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "eApp.CommandService.Api/eApp.CommandService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "eApp.CommandService.Api/eApp.CommandService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eApp.CommandService.Api.dll"]
