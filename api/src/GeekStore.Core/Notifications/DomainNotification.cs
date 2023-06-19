namespace GeekStore.Core.Notifications
{
    public class DomainNotification
    {
        public DomainNotification(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public string Key { get; }
        public string Message { get; }
    }
}
