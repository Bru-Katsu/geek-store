using GeekStore.Identity.WebAPI.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApiConfiguration(builder.Configuration);

var app = builder.Build();

app.UseApplicationConfiguration();

app.Run();
