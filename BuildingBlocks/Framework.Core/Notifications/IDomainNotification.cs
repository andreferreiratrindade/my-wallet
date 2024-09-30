using FluentValidation.Results;
using Framework.Core.DomainObjects;

namespace Framework.Core.Notifications
{
    public interface IDomainNotification
    {
        void SetCorrelationId(CorrelationIdGuid correlationId);
        CorrelationIdGuid GetCorrelationId();
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
