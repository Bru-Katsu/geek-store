using MediatR;

namespace GeekStore.Core.Messages
{
    public abstract class Event : Message, INotification
    {
        protected Event()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; set; }
    }
}
