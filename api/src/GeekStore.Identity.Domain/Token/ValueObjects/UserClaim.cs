namespace GeekStore.Identity.Domain.Token.ValueObjects
{
    public class UserClaim
    {
        public UserClaim(string value, string type)
        {
            Value = value;
            Type = type;
        }

        public string Value { get; }
        public string Type { get; }
    }
}