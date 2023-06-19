using GeekStore.Core.DI;
using GeekStore.Product.Data.Configurations;
using GeekStore.Product.WebAPI.DI;
using GeekStore.Product.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.UseSqlServer(builder.Configuration);

builder.Services.AddCoreServices()
                .AddDataServices()
                .AddServices()
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>()); ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Product API";
    });
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();
app.UseCors(options =>
{
    options
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins("http://localhost:4200");
});
app.MapControllers();

app.Run();
