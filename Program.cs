using JwtAuthWebApi.Data;
using JwtAuthWebApi.Extensions;
using JwtAuthWebApi.Middlewares;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlite(connectionString);
    options.UseSnakeCaseNamingConvention();
});

// Configuration Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) 
    .CreateLogger();

// add Serilog as the logging provider
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenExt();
builder.Services.AddServices();
builder.Services.AddJwtConfig(builder.Configuration);
builder.Services.AddControllers(options => options.ModelValidatorProviders.Clear());
builder.Services.AddFluentValidationRulesToSwagger();

var app = builder.Build();

app.UseMiddleware<ErrorExceptionMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();