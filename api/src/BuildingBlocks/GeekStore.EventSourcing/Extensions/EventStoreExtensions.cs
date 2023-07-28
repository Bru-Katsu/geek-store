using EventStore.Client;
using GeekStore.Core.Messages;
using Newtonsoft.Json;
using System.Text;

namespace GeekStore.EventSourcing.Extensions
{
    internal static class EventStoreExtensions
    {
        internal static IEnumerable<EventData> ToEventData<TEvent>(this TEvent @event) where TEvent : Event
        {
            yield return new EventData(Uuid.NewUuid(), @event.MessageType, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event)));
        }
    }
}
