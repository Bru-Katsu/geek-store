using GeekStore.Identity.Domain.User;

namespace GeekStore.Identity.Application.Users.Events
{
    public class UserDeletedEvent : UserEventBase
    {
        public UserDeletedEvent(User user) : base(user)
        {
        }
    }
}
