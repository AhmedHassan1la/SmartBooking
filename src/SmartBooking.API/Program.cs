using AutoMapper; // Add this using directive at the top of the file
using Microsoft.EntityFrameworkCore;
using SmartBooking.Application.Mapping;
using SmartBooking.Application.Mappings;
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

// Ensure you have the AutoMapper.Extensions.Microsoft.DependencyInjection package installed
// You can install it via NuGet Package Manager or using the following command:
// dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
builder.Services.AddAutoMapper(typeof(DoctorProfileMapping).Assembly);
builder.Services.AddAutoMapper(typeof(ClinicProfileMapping));


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
