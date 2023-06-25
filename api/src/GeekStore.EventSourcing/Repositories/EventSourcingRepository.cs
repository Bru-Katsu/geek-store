using EventStore.Client;
using GeekStore.Core.EventSourcing;
using GeekStore.Core.EventSourcing.Repositories;
using GeekStore.Core.Messages;
using GeekStore.EventSourcing.Extensions;
using GeekStore.EventSourcing.Services;
using Newtonsoft.Json;
using System.Text;

namespace GeekStore.EventSourcing.Repositories
{
    internal class EventSourcingRepository : IEventSourcingRepository
    {
        private readonly IEventStoreService _eventStoreService;

        public EventSourcingRepository(IEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async IAsyncEnumerable<StoredEvent> GetEventsAsync(Guid aggregateId, int start = 0, int take = 500)
        {
            var cli = _eventStoreService.GetClient();
            var e = cli.ReadStreamAsync(Direction.Backwards, aggregateId.ToString(), StreamPosition.Start, take);

            await foreach (var resolvedEvent in e)
            {
                var dataEncoded = Encoding.UTF8.GetString(resolvedEvent.OriginalEvent.Data.ToArray());
                var jsonData = JsonConvert.DeserializeObject<Event>(dataEncoded);

                yield return new StoredEvent(resolvedEvent.OriginalEvent.EventId.ToGuid(), resolvedEvent.Event.EventType, jsonData.Timestamp, dataEncoded);                
            }
        }

        public async Task SaveEventAsync<TEvent>(TEvent @event) where TEvent : Event
        {
            if (@event == null) 
                throw new ArgumentNullException(nameof(@event));

            var cli = _eventStoreService.GetClient();
            await cli?.AppendToStreamAsync(@event.AggregateId.ToString(), StreamState.Any, @event.ToEventData());
        }
    }
}
