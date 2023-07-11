using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Security.JwtExtensions;
using System.IdentityModel.Tokens.Jwt;

namespace GeekStore.WebApi.Core.Identity
{
    public static class JwtConfig
    {
        public static IServiceCollection AddAuthConfiguration(this IServiceCollection builder, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            builder.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            builder.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            ).AddJwtBearer(bearerOptions =>
            {
                bearerOptions.IncludeErrorDetails = true;
                bearerOptions.RequireHttpsMetadata = true;
                bearerOptions.SaveToken = true;
                bearerOptions.SetJwksOptions(new JwkOptions(appSettings.JksUrlAuthentication));
            });

            return builder;
        }

        public static IApplicationBuilder UseAuthConfiguration(this IApplicationBuilder webApplication)
        {
            webApplication.UseAuthentication();
            webApplication.UseAuthorization();

            return webApplication;
        }
    }
}
