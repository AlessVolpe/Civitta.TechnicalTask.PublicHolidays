using Civitta.TechnicalTask.PublicHolidays.Models;
using Civitta.TechnicalTask.PublicHolidays.Services;
using Civitta.TechnicalTask.PublicHolidays.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(
        options => options.UseSqlServer("name=ConnectionStrings:Civitta.TechnicalTask.PublicHolidays.Cloud"));
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IHolidayService, Civitta.TechnicalTask.PublicHolidays.Services.NotImplementedException>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c => {
    c.SwaggerDoc("v1", new OpenApiInfo {
        Version = "v1",
        Title = "Civitta Technical Task: Public Holidays",
        Description = "Uses Kayaposoft's enrico to get countries, holiday by month in a certain country and the maximum amount of free days in a certain month"
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

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
