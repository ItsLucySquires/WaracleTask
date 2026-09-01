using Microsoft.EntityFrameworkCore;
using WaracleTask.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<HotelsService>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<RoomsService>();


var connection = string.Empty;

// Configure the HTTP request pipeline and services before building the app.
builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.json");
connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

builder.Services.AddDbContext<WaracleTaskContext>(options =>
    options.UseSqlServer(connection));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

// Map controllers so endpoints (and Swagger JSON) are available
app.MapControllers();

// Start the application
app.Run();





































