using GeekStore.Identity.Data.Context;
using GeekStore.Identity.Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GeekStore.Identity.WebAPI.Configurations
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddJwksManager()
                    .PersistKeysToDatabaseStore<IdentityContext>()
                    .UseJwtValidation();

            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<User>()
                    .AddRoles<IdentityRole<Guid>>()
                    .AddEntityFrameworkStores<IdentityContext>()
                    .AddDefaultTokenProviders();

            return services;
        }
    }
}