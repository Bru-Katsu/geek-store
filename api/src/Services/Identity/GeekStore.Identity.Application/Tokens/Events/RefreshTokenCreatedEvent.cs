using GeekStore.Core.Messages;
using GeekStore.Identity.Domain.Token;

namespace GeekStore.Identity.Application.Tokens.Events
{
    public class RefreshTokenCreatedEvent : Event
    {
        public RefreshTokenCreatedEvent(RefreshToken refreshToken)
        {
            AggregateId = refreshToken.Id;
            UserName = refreshToken.UserName;
            ExpiresIn = refreshToken.ExpirationDate;
        }

        public string UserName { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}
