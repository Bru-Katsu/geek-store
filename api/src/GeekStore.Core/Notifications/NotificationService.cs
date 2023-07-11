using FluentValidation.Results;

namespace GeekStore.Core.Notifications
{
    internal class NotificationService : INotificationService
    {
        private readonly List<DomainNotification> _notifications;

        public NotificationService()
        {
            _notifications = new List<DomainNotification>();
        }

        public IEnumerable<DomainNotification> GetNotifications()
        {
            return _notifications;
        }

        public void AddNotification(string key, string message)
        {
            _notifications.Add(new DomainNotification(key, message));
        }

        public void AddNotification(DomainNotification domainNotification)
        {
            _notifications.Add(domainNotification);
        }

        public void AddNotifications(ValidationResult validationResult)
        {
            foreach (var item in validationResult.Errors)
                _notifications.Add(new DomainNotification(item.PropertyName, item.ErrorMessage));
        }

        public void AddNotifications(IEnumerable<DomainNotification> domainNotifications)
        {
            foreach (var item in domainNotifications)
                _notifications.Add(item);
        }

        public void Clear()
        {
            _notifications.Clear();
        }

        public bool HasNotifications()
        {
            return _notifications.Any();
        }
    }
}
