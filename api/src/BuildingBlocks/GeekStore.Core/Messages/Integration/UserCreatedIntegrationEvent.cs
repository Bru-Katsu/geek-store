namespace GeekStore.Core.Messages.Integration
{
    public class UserCreatedIntegrationEvent : IntegrationEvent
    {
        public UserCreatedIntegrationEvent(Guid id, string name, string surname, string email, string document, DateTime birthday)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            Document = document;
            Birthday = birthday;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Surname { get; }
        public string Email { get;}
        public string Document { get; }
        public DateTime Birthday { get; }
    }
}
