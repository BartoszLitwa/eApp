namespace eApp.PlatformService.Api;

public class SqlConfig
{
    public const string SqlSection = "Sql";

    public string ConnectionString { get; set; } = default!;
}