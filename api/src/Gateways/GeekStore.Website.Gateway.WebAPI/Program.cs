using GeekStore.Website.Gateway.WebAPI.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Configuration.AddEnvironmentVariables();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApiConfiguration(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseApplicationConfiguration();

app.MapControllers();
app.Run();