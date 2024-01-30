using System.Text.Json.Serialization;
using eApp.PlatformService.Domain.Models;

namespace eApp.PlatformService.Api.Utils;

[JsonSerializable(typeof(Platform[]))]
public partial class AppJsonSerializerContext : JsonSerializerContext
{
}