using GeekStore.Product.Data.Configurations;
using GeekStore.WebApi.Core.Identity;
using GeekStore.Product.WebAPI.Configuration;
using GeekStore.WebApi.Core.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApiConfiguration()
                .AddSqlServer(builder.Configuration)
                .AddEndpointsApiExplorer()
                .AddSwaggerConfiguration()
                .AddAuthConfiguration(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwaggerConfiguration();

app.UseHttpsRedirection();

app.UseCors(options =>
{
    options
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins("http://localhost:4200");
});

app.UseRouting();

app.UseAuthConfiguration();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
