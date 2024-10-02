using FluentValidation.Results;
using Framework.Core.DomainObjects;

namespace Framework.Core.Notifications
{
    public interface IDomainNotification
    {
        void SetCorrelationId(CorrelationId correlationId);
        CorrelationId GetCorrelationId();
        IReadOnlyCollection<NotificationMessage> Notifications { get; }
        bool HasNotifications { get; }

        bool HasNotificationWithException{get;}
        void AddNotification(Exception ex);
        void AddNotification(string key, string message);
        void AddNotification(IEnumerable<NotificationMessage> notifications);
        void AddNotification(ValidationResult validationResult);

        ValidationResult GetValidationResult();
    }
}
