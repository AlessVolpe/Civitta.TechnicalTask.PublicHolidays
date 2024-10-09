using Civitta.TechnicalTask.PublicHolidays.Models;
using Civitta.TechnicalTask.PublicHolidays.Services;
using Civitta.TechnicalTask.PublicHolidays.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(
        options => options.UseSqlServer("name=ConnectionStrings:Civitta.TechnicalTask.PublicHolidays.Cloud"));
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IHolidayService, HolidayService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
