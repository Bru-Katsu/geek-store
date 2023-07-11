using GeekStore.Core.Queries;
using GeekStore.Identity.Application.Tokens.ViewModels;

namespace GeekStore.Identity.Application.Tokens.Queries
{
    public class RefreshTokenQuery : IQuery<RefreshTokenViewModel>
    {
        public Guid Token { get; set; }
    }
}
