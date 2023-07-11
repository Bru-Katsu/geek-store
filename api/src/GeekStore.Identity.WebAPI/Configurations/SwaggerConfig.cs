using Microsoft.OpenApi.Models;

namespace GeekStore.Identity.WebAPI.Configurations
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "GeekStore - Identity",
                    Description = "Identity API",
                    Contact = new OpenApiContact() { Name = "Bruno Alves", Email = "bruno.alves5009@gmail.com", Url = new Uri("https://github.com/Bru-Katsu") },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
                });
            });

            return services;
        }


        public static WebApplication UseSwaggerConfiguration(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.DocumentTitle = "Identity API";
                });
            }

            return app;
        }
    }
}