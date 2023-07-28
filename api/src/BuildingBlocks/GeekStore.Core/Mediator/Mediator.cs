using GeekStore.Core.EventSourcing.Repositories;
using GeekStore.Core.Messages;
using MediatR;

namespace GeekStore.Core.Mediator
{
    public class MediatorProxy : MediatR.Mediator
    {
        private readonly IEventSourcingRepository _eventSourcingRepository;
        public MediatorProxy(IServiceProvider serviceProvider, 
                             INotificationPublisher publisher, 
                             IEventSourcingRepository eventSourcingRepository) : base(serviceProvider, publisher)
        {
            _eventSourcingRepository = eventSourcingRepository;
        }

        protected override async Task PublishCore(IEnumerable<NotificationHandlerExecutor> handlerExecutors, INotification notification, CancellationToken cancellationToken)
        {
            if (notification is Event evento)
                await InvokeSaveEventAsync(evento);            

            await base.PublishCore(handlerExecutors, notification, cancellationToken);
        }

        private async Task InvokeSaveEventAsync(Event evento)
        {
            var saveEventAsyncMethod = typeof(IEventSourcingRepository).GetMethod(nameof(IEventSourcingRepository.SaveEventAsync))
                                                                       .MakeGenericMethod(evento.GetType());

            if (saveEventAsyncMethod != null)
            {
                await (Task)saveEventAsyncMethod.Invoke(_eventSourcingRepository, new object[] { evento });
            }
        }
    }
}
