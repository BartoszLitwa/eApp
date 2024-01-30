FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://0.0.0.0:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ./PlatformService/eApp.PlatformService.Api/eApp.PlatformService.Api.csproj ./PlatformService/eApp.PlatformService.Api/
COPY ./PlatformService/eApp.PlatformService.Domain/eApp.PlatformService.Domain.csproj ./PlatformService/eApp.PlatformService.Domain/
COPY ../Shared/eApp.Common/eApp.Common.csproj ./Shared/eApp.Common/
RUN dotnet restore "PlatformService/eApp.PlatformService.Api/eApp.PlatformService.Api.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "PlatformService/eApp.PlatformService.Api/eApp.PlatformService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PlatformService/eApp.PlatformService.Api/eApp.PlatformService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eApp.PlatformService.Api.dll"]
