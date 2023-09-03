using GeekStore.Identity.Data.Context;
using GeekStore.Identity.Domain.User;
using GeekStore.WebApi.Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GeekStore.Identity.WebAPI.Configurations
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddJwksManager()
                    .PersistKeysToDatabaseStore<IdentityContext>()
                    .UseJwtValidation();

            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection")));

            services.AddDefaultIdentity<User>()
                    .AddRoles<IdentityRole<Guid>>()
                    .AddEntityFrameworkStores<IdentityContext>()
                    .AddDefaultTokenProviders();

            return services;
        }
    }
}