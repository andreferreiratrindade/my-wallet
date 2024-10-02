using FluentValidation.Results;
using Framework.Core.DomainObjects;

namespace Framework.Core.Notifications
{
    public class DomainNotification : IDomainNotification
    {
        private readonly List<NotificationMessage> _notifications;
        private CorrelationId _CorrelationId;

        public DomainNotification()
        {
            _notifications = new List<NotificationMessage>();
        }

        public IReadOnlyCollection<NotificationMessage> Notifications => _notifications;

        public bool HasNotifications => _notifications.Any();

        public bool HasNotificationWithException => _notifications.Any(x=> x.NotificationMessageType == NotificationMessageType.Excetpion);

        public void AddNotification(string key, string message)
        {
            _notifications.Add(new NotificationMessage(key, message));
        }

        public void AddNotification(Exception ex){
            _notifications.Add(new NotificationMessage("Error", "Somenthing inexpeted happened" ));
            _notifications.Add(new NotificationMessage("Error", $"{ex.Message}: {ex.StackTrace}", NotificationMessageType.Excetpion ));

        }

        public void AddNotification(IEnumerable<NotificationMessage> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void AddNotification(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                _notifications.Add(new NotificationMessage(error.ErrorCode, error.ErrorMessage));
            }
        }

        public CorrelationId GetCorrelationId()
        {
            return _CorrelationId;
        }

        public ValidationResult GetValidationResult(){
            return new ValidationResult( _notifications.Select(x=> new ValidationFailure("",x.Value)).ToList());
        }

        public void SetCorrelationId(CorrelationId correlationId)
        {
            _CorrelationId = correlationId;
        }
    }
}
