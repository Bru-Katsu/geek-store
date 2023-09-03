using GeekStore.Identity.Domain.User;
using Newtonsoft.Json;

namespace GeekStore.Identity.Application.Users.Events
{
    public class UserCreatedEvent : UserEventBase
    {
        public UserCreatedEvent(string name, string surname, string document, DateTime birthday, User user) : base(user)
        {
            Name = name;
            Surname = surname;
            Document = document;
            Birthday = birthday;
        }

        [JsonIgnore]
        public string Name { get; set; }

        [JsonIgnore]
        public string Surname { get; set; }

        [JsonIgnore]
        public string Document { get; set; }

        [JsonIgnore]
        public DateTime Birthday { get; set; }
    }
}
