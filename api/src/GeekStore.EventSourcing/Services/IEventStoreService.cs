using EventStore.Client;
using Microsoft.Extensions.Configuration;

namespace GeekStore.EventSourcing.Services
{
    public interface IEventStoreService
    {
        EventStoreClient GetClient();
    }

    internal class EventStoreService : IEventStoreService
    {
        private EventStoreClient _connection;
        private IConfiguration _configuration;

        public EventStoreService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public EventStoreClient GetClient()
        {
            if (_connection is null)
            {
                var s = EventStoreClientSettings.Create(_configuration.GetConnectionString("EventStoreConnection"));
                _connection = new EventStoreClient(s);                
            }

            return _connection;
        }
    }
}
