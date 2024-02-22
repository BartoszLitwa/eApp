namespace eApp.CommandService.Api.EventProcessing;

public interface IEventProcessor
{
    Task ProcessEvent(string message, CancellationToken cancellationToken);
}