using GeekStore.Identity.Domain.User;

namespace GeekStore.Identity.Application.Users.Events
{
    public class UserCreatedEvent : UserEventBase
    {
        public UserCreatedEvent(User user) : base(user)
        { }
    }
}
