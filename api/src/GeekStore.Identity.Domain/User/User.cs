using Microsoft.AspNetCore.Identity;

namespace GeekStore.Identity.Domain.User
{
    public class User : IdentityUser<Guid>
    {
        public User(string email)
        {
            UserName = email;
            Email = email;            
        }
    }
}
