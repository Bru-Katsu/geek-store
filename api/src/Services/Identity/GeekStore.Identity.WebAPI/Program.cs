using GeekStore.Identity.Data.Context;
using GeekStore.Identity.WebAPI.Configurations;
using GeekStore.WebApi.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApiConfiguration(builder.Configuration);
builder.Configuration.AddEnvironmentVariables();

var app = builder.Build();

app.UseApplicationConfiguration()
   .ApplyMigrationsFrom<IdentityContext>();

app.Run();
