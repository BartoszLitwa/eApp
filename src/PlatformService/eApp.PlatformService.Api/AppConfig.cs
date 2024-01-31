namespace eApp.PlatformService.Api;

public class AppConfig
{
    public const string Section = "AppConfig";

    public string CommandServiceUrl { get; set; }
    
    public ConnectionStrings ConnectionStrings { get; set; }
}

public class ConnectionStrings
{
    public const string Section = "ConnectionStrings";
    
    public string PlatformMssql { get; set; }
}