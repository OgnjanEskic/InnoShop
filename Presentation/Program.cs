using Application.Extensions;
using Application.Validators;
using Carter;
using Domain.Interfaces;
using Domain.Models.Requests;
using EntityFramework.Exceptions.SqlServer;
using FluentValidation;
using Infrastructure;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IValidator<LocationRequest>, AddLocationRequestValidator>();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCarter();

builder.Services.AddDbContext<LocationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING"))
    .UseExceptionProcessor());

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCarter();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
