using GeekStore.Core.EventSourcing.Repositories;
//using GeekStore.Core.Mediator;
using GeekStore.Core.Messages;
//using GeekStore.Core.Tests.Mediator.Models;
using Moq;
using Moq.AutoMock;
using Lamar;
using MediatR;
using MediatR.NotificationPublishers;
using GeekStore.Core.Mediator;

namespace GeekStore.Core.Tests.Mediator
{
    public class TestEvent : Event
    { }

    public class TestHandler : INotificationHandler<TestEvent>
    {
        public Task Handle(TestEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    public class DummyNotification : INotification
    { }

    public class DummyNotificationHandler : INotificationHandler<DummyNotification>
    {
        public Task Handle(DummyNotification notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    public class MediatorProxyTests
    {
        [Fact]
        [Trait(nameof(MediatorProxy), nameof(Event))]
        public async Task MediatorProxy_IsEvent_ShouldBeSavedInEventSourcing()
        {
            var mock = new AutoMocker();
            var eventSoucingRepo = new Mock<IEventSourcingRepository>();

            var container = new Container(cfg =>
            {
                cfg.Scan(scanner =>
                {
                    scanner.AssemblyContainingType(typeof(MediatorProxyTests));
                    scanner.IncludeNamespaceContainingType<INotification>();
                    scanner.WithDefaultConventions();
                    scanner.AddAllTypesOf(typeof(INotificationHandler<>));
                });

                cfg.For<IEventSourcingRepository>()
                   .Use(eventSoucingRepo.Object);

                cfg.For<INotificationPublisher>()
                   .Use<ForeachAwaitPublisher>();

                cfg.For<IMediator>()
                   .Use<MediatorProxy>();
            });

            var mediator = container.GetInstance<IMediator>();

            mock.Use(mediator);
            mock.Use(eventSoucingRepo);

            var proxy = mock.Get<IMediator>();
            await proxy.Publish(new TestEvent());

            mock.GetMock<IEventSourcingRepository>()
                .Verify(repository => repository.SaveEventAsync(It.IsAny<Event>()), Times.Once);
        }

        [Fact]
        [Trait(nameof(MediatorProxy), nameof(INotification))]
        public async Task MediatorProxy_IsNotEvent_ShouldIgnoreSaveEventSourcing()
        {
            var mock = new AutoMocker();
            var eventSoucingRepo = new Mock<IEventSourcingRepository>();

            var container = new Container(cfg =>
            {
                cfg.Scan(scanner =>
                {
                    scanner.AssemblyContainingType(typeof(MediatorProxyTests));
                    scanner.IncludeNamespaceContainingType<INotification>();
                    scanner.WithDefaultConventions();
                    scanner.AddAllTypesOf(typeof(INotificationHandler<>));
                });

                cfg.For<IEventSourcingRepository>()
                   .Use(eventSoucingRepo.Object);

                cfg.For<INotificationPublisher>()
                   .Use<ForeachAwaitPublisher>();

                cfg.For<IMediator>()
                   .Use<MediatorProxy>();
            });

            var mediator = container.GetInstance<IMediator>();

            mock.Use(mediator);
            mock.Use(eventSoucingRepo);

            var proxy = mock.Get<IMediator>();
            await proxy.Publish(new DummyNotification());

            mock.GetMock<IEventSourcingRepository>()
                .Verify(repository => repository.SaveEventAsync(It.IsAny<Event>()), Times.Never);
        }
    }
}
