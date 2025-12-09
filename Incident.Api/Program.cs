using Incident.Application.Commands.CreateIncident;
using Incident.Domain.Interfaces;
using Incident.Infrastructure.Persistence;
using Incident.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog
builder.Host.UseSerilog((ctx, lc) =>
    lc.WriteTo.Console()
      .ReadFrom.Configuration(ctx.Configuration));

// DB InMemory
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("IncidentsDb"));

// Repos
builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();

// MediatR
builder.Services.AddMediatR(typeof(CreateIncidentHandler).Assembly);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSerilogRequestLogging();

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
