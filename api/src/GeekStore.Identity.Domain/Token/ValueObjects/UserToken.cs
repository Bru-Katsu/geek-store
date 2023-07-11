namespace GeekStore.Identity.Domain.Token.ValueObjects
{
    public class UserToken
    {
        public UserToken(Guid id, string email, IEnumerable<UserClaim> claims)
        {
            Id = id;
            Email = email;
            Claims = claims;
        }

        public Guid Id { get; }
        public string Email { get; }
        public IEnumerable<UserClaim> Claims { get; }
    }
}