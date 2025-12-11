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
using Incident.Api.Middleware;
using Incident.Application.Behaviors;

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

builder.Services.AddValidatorsFromAssemblyContaining<UpdateIncidentValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseSerilogRequestLogging();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
