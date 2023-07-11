using GeekStore.Identity.Domain.Token;
using GeekStore.Identity.Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Security.Jwt.Core.Model;
using NetDevPack.Security.Jwt.Store.EntityFrameworkCore;

namespace GeekStore.Identity.Data.Context
{
    public class IdentityContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>, ISecurityKeyContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        { }

        public DbSet<KeyMaterial> SecurityKeys { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public override void Dispose()
        {
            base.Dispose();
        }

        public override ValueTask DisposeAsync()
        {
            return base.DisposeAsync();
        }
    }
}
