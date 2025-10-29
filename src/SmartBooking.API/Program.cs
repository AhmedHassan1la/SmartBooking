using AutoMapper; // Add this using directive at the top of the file
using Microsoft.EntityFrameworkCore;
using SmartBooking.Application.Mapping;
using SmartBooking.Application.Mappings;
using SmartBooking.Application.Services.Clinics;
using SmartBooking.Application.Services.Doctors;
using SmartBooking.Application.Services.Specialities;
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
        License = new()
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/license/mit/")
        }
    });
});

builder.Services.AddInfrastructureConfiguration();

builder.Services.AddAutoMapper(typeof(DoctorProfileMapping).Assembly);


// Configure DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IClinicService, ClinicService>();
builder.Services.AddScoped<ISpecialityService, SpecialityService>();

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
