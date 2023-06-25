using GeekStore.Core.Messages;
using System.Collections.Generic;

namespace GeekStore.Core.EventSourcing.Repositories
{
    public interface IEventSourcingRepository
    {
        IAsyncEnumerable<StoredEvent> GetEventsAsync(Guid aggregateId, int start = 0, int take = 500);
        Task SaveEventAsync<TEvent>(TEvent @event) where TEvent : Event;
    }
}
