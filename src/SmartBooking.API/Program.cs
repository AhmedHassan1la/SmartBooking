using Microsoft.EntityFrameworkCore;
using SmartBooking.Infrastructure;
using SmartBooking.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Add Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "SmartBooking API",
        Version = "v1",
        Description = "SmartBooking Platform API Documentation",
        Contact = new()
        {
            Name = "Your Name",
            Email = "yourname@gmail.com",
            Url = new Uri("https://yourwebsite.com")
        },
        License = new()
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/license/mit/")
        }
    });
});
builder.Services.AddInfrastructureConfiguration();
// Configure DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
