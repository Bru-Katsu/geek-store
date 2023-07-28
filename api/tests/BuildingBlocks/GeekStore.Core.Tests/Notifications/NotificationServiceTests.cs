using Bogus;
using FluentValidation;
using GeekStore.Core.Notifications;

namespace GeekStore.Core.Tests.Notifications
{
    public class ValidationModel
    {
        public ValidationModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ValidationModelValidator : AbstractValidator<ValidationModel>
    {
        public ValidationModelValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Deve ser maior que zero");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Não pode ser vazio");
        }
    }

    public class NotificationServiceTests
    {
        private readonly Faker _faker;

        public NotificationServiceTests()
        {
            _faker = new Faker();
        }

        [Fact]
        [Trait(nameof(NotificationService), "Add DomainNotification")]
        public void AddDomainNotification_ShouldContainsInService()
        {
            //Arrange
            var service = new NotificationService();

            //Act
            service.AddNotification(new DomainNotification(_faker.Random.Word(), _faker.Random.Word()));

            //Assert
            Assert.Single(service.GetNotifications());
            Assert.True(service.HasNotifications());
        }

        [Fact]
        [Trait(nameof(NotificationService), "Add ValidationResults")]
        public void AddValidationResultNotifications_ShouldContainsInService()
        {
            //Arrange
            var service = new NotificationService();
            var validationModel = new ValidationModel(0, string.Empty);
            var validator = new ValidationModelValidator();

            //Act
            service.AddNotifications(validator.Validate(validationModel));

            //Assert
            Assert.True(service.GetNotifications().Any());
            Assert.True(service.HasNotifications());
        }

        [Fact]
        [Trait(nameof(NotificationService), "Add KeyPairNotification")]

        public void AddNotificationKeyPair_ShouldContainsInService()
        {
            //Arrange
            var service = new NotificationService();

            //Act
            service.AddNotification(_faker.Random.Word(), _faker.Random.Word());

            //Assert
            Assert.True(service.GetNotifications().Any());
            Assert.True(service.HasNotifications());
        }

        [Fact]
        [Trait(nameof(NotificationService), "Add DomainNotification Enumerable")]
        public void AddDomainNotificationsEnumerable_ShouldContainsInService()
        {
            //Arrange
            var service = new NotificationService();
            var notifications = _faker.Make(10, () => new DomainNotification(_faker.Random.Word(), _faker.Random.Word()));

            //Act
            service.AddNotifications(notifications);

            //Assert
            Assert.True(service.GetNotifications().Any());
            Assert.True(service.HasNotifications());
        }

        [Fact(DisplayName = "")]
        [Trait(nameof(NotificationService), nameof(NotificationService.Clear))]

        public void ClearNotifications_ShouldRemoveAllNotifications()
        {
            //Arrange
            var service = new NotificationService();
            var notifications = _faker.Make(10, () => new DomainNotification(_faker.Random.Word(), _faker.Random.Word()));

            service.AddNotifications(notifications);

            //Act
            service.Clear();

            //Assert
            Assert.Empty(service.GetNotifications());
            Assert.False(service.HasNotifications());
        }
    }
}
