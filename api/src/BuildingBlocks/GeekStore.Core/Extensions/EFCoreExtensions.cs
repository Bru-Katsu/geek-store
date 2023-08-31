using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Core.Extensions
{
    public static class EFCoreExtensions
    {
        public static void ApplyMigrations<TContext>(this IServiceProvider provider) where TContext : DbContext
        {            
            using (var scope = provider.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<TContext>();
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
