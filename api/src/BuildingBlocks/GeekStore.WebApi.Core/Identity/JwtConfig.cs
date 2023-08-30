using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NetDevPack.Security.JwtExtensions;
using System.IdentityModel.Tokens.Jwt;

namespace GeekStore.WebApi.Core.Identity
{
    public static class JwtConfig
    {
        public static IServiceCollection AddAuthConfiguration(this IServiceCollection builder, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AuthenticationSettings");
            builder.Configure<AuthenticationSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AuthenticationSettings>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            builder.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            ).AddJwtBearer(bearerOptions =>
            {
                bearerOptions.IncludeErrorDetails = true;

                bearerOptions.RequireHttpsMetadata = false;
                bearerOptions.BackchannelHttpHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                }; 
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
