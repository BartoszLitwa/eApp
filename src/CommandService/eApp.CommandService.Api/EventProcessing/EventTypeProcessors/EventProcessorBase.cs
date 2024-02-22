using AutoMapper;

namespace eApp.CommandService.Api.EventProcessing.EventTypeProcessors;

public abstract class EventProcessorBase
{
    public virtual PlatformEventType EventType => PlatformEventType.Undetermined;

    public virtual async ValueTask<bool> ProcessEvent(IServiceProvider serviceProvider, IMapper mapper, string message, CancellationToken cancellationToken)
    {
        return true;
    }
}