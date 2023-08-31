using GeekStore.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace GeekStore.WebApi.Core.Extensions
{
    public static class EFCoreExtensions
    {
        public static WebApplication ApplyMigrationsFrom<TContext>(this WebApplication app) where TContext : DbContext
        {
            app.Services.ApplyMigrations<TContext>();
            return app;
        }
    }
}
