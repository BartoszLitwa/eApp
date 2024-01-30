using System.Text;
using System.Text.Json;
using eApp.PlatformService.Api.Dtos;
using Microsoft.Extensions.Options;

namespace eApp.PlatformService.Api.SyncDataServices.Http;

public class HttpCommandDataClient(HttpClient httpClient, IOptions<AppConfig> appConfig) : ICommandDataClient
{
    public async Task SendPlatformToCommand(PlatformReadDto platform)
    {
        var httpContent = new StringContent(
            JsonSerializer.Serialize(platform),
            Encoding.UTF8,
            "application/json");

        var response = await httpClient.PostAsync($"{appConfig.Value.CommandServiceUrl}/api/platforms", httpContent);

        Console.WriteLine($"--> Sync POST to CommandService was {(response.IsSuccessStatusCode ? "Ok" : "Not OK")}!");
    }
}