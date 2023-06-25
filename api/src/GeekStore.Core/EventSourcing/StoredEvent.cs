namespace GeekStore.Core.EventSourcing
{
    public class StoredEvent
    {
        public StoredEvent(Guid id, string type, DateTime timestamp, string data)
        {
            Id = id;
            Type = type;
            Timestamp = timestamp;
            Data = data;
        }

        public Guid Id { get; private set; }
        public string Type { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string Data { get; private set; }
    }
}
