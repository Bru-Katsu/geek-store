using GeekStore.Core.Messages;
using GeekStore.Identity.Domain.User;

namespace GeekStore.Identity.Application.Users.Events
{
    public abstract class UserEventBase : Event
    {
        public UserEventBase(User user)
        {
            AggregateId = user.Id;
            Email = user.Email ?? string.Empty;
            PhoneNumber = user.PhoneNumber ?? string.Empty;
        }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
