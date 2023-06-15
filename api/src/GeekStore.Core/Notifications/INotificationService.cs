using FluentValidation.Results;

namespace GeekStore.Core.Notifications
{
    public interface INotificationService
    {
        IEnumerable<DomainNotification> GetNotifications();

        void AddNotification(string key, string message);
        void AddNotification(DomainNotification domainNotification);

        void AddNotifications(ValidationResult validationResult);
        void AddNotifications(IEnumerable<DomainNotification> domainNotifications);
        void Clear();
    }
}
