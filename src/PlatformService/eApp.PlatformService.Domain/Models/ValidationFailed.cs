namespace eApp.PlatformService.Domain.Models;

public record ValidationFailed(IEnumerable<string> Errors)
{
    public ValidationFailed(string error) : this(new[] { error })
    {
    }
}