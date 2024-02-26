using FluentValidation.AspNetCore;
using FluentValidation;
using GokstadHageVennerAPI.Data;
using GokstadHageVennerAPI.Extensions;
using GokstadHageVennerAPI.Middleware;
using GokstadHageVennerAPI.Repository;
using GokstadHageVennerAPI.Repository.Interfaces;
using GokstadHageVennerAPI.Services;
using GokstadHageVennerAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.RegisterMappers();
builder.RegisterServices();
builder.RegisterRepositories();

builder.AddSwaggerWithBasicAuthentication();

builder.Services.AddDbContext<GokstadHageVennerDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0))));

builder.Services.AddScoped<GlobalExceptionMiddleware>();

builder.Services.AddScoped<GokstadHageVennerBasicAuthentication>();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation(config => config.DisableDataAnnotationsValidation = true);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseSerilogRequestLogging();
app.UseMiddleware<GokstadHageVennerBasicAuthentication>();

app.UseAuthorization();

app.MapControllers();

app.Run();
