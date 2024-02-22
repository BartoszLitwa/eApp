using System.Reflection;
using System.Text.Json;
using AutoMapper;
using eApp.CommandService.Api.Dtos;
using eApp.CommandService.Api.EventProcessing.EventTypeProcessors;

namespace eApp.CommandService.Api.EventProcessing;

public class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;
    private readonly Dictionary<PlatformEventType, EventProcessorBase> _eventProcessors;
    
    public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
    {
        _scopeFactory = scopeFactory;
        _mapper = mapper;
        _eventProcessors = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && !type.IsInterface)
            .Where(type => type.IsSubclassOf(typeof(EventProcessorBase)))
            .Select(type =>
            {
                var eventProcessor = Activator.CreateInstance(type) as EventProcessorBase;
                return new
                {
                    EventType = eventProcessor!.EventType,
                    EventProcessor = eventProcessor
                };
            })
            .ToDictionary(x => x.EventType, x => x.EventProcessor);
    }


    public async Task ProcessEvent(string message, CancellationToken cancellationToken)
    {
        var eventType = DetermineEvent(message);
        var processor = _eventProcessors[eventType];
        
        await using var scope = _scopeFactory.CreateAsyncScope();
        await processor.ProcessEvent(scope.ServiceProvider, _mapper, message, cancellationToken);
    }

    private PlatformEventType DetermineEvent(string notificationMessage)
    {
        var eventDeserialized = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);
        var eventTypeDetermined = Enum.TryParse(typeof(PlatformEventType), eventDeserialized.Event, out var eventTypeObject);
        var eventType = eventTypeDetermined ? (PlatformEventType)eventTypeObject : PlatformEventType.Undetermined;
        
        Console.WriteLine($"--> Determined event: {eventType}");
        return eventType;
    }
}