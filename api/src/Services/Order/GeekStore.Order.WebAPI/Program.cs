using GeekStore.Order.Data.Context;
using GeekStore.Order.WebAPI.Configuration;
using GeekStore.WebApi.Core.Identity;
using GeekStore.WebApi.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Configuration.AddEnvironmentVariables();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApiConfiguration(builder.Configuration)
                .AddSwaggerConfiguration()
                .AddAuthConfiguration(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwaggerConfiguration()
   .UseApiConfiguration()
   .ApplyMigrationsFrom<OrderDataContext>();

app.MapControllers();
app.Run();
