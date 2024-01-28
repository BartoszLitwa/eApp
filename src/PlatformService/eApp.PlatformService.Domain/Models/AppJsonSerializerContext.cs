using System.Text.Json.Serialization;

namespace eApp.PlatformService.Domain.Models;

[JsonSerializable(typeof(Platform[]))]
public partial class AppJsonSerializerContext : JsonSerializerContext
{
}