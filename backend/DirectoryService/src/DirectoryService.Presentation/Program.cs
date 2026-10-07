using DirectoryService.Infrastructure.Postgres;
using DirectoryService.Presentation;
using DirectoryService.Presentation.Middlewares;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureDependencies(builder.Configuration); // Add infrastructure dependencies
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks(); // health check endpoint from Microsoft SDK
builder.Services.AddDbContext<DirectoryServiceDbContext>(options
    => options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"))); // Add postgres database context
builder.Services.AddHealthChecks()
    .AddDbContextCheck<DirectoryServiceDbContext>(); // Add health check for the database context

builder.Services.AddProgrammDependencies(); // Add application dependencies

var app = builder.Build();

app.UseExceptionMiddleware(); // Add exception handling middleware

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