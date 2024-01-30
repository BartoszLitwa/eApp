namespace eApp.PlatformService.Api;

public class AppConfig
{
    public const string Section = "AppConfig";

    public string ConnectionString { get; set; }
    public string CommandServiceUrl { get; set; }
}