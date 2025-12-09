using Incident.Application.Commands.CreateIncident;
using Incident.Domain.Interfaces;
using Incident.Infrastructure.Persistence;
using Incident.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using FluentValidation;
using FluentValidation.AspNetCore;
using Incident.Application.Validators;

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog((ctx, lc) =>
    lc.WriteTo.Console()
      .ReadFrom.Configuration(ctx.Configuration));


builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("IncidentsDb"));


builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();


builder.Services.AddMediatR(typeof(CreateIncidentHandler).Assembly);


builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<CreateIncidentValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSerilogRequestLogging();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
