using DirectoryService.Infrastructure.Postgres;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks(); // health check endpoint from Microsoft SDK
builder.Services.AddDbContext<DirectoryServiceDbContext>(options
    => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); // Add postgres database context
builder.Services.AddHealthChecks().AddDbContextCheck<DirectoryServiceDbContext>(); // Add health check for the database context

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.MapHealthChecks("/Health");
}

app.UseAuthorization();

app.MapControllers();

app.Run();