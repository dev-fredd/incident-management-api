using FluentValidation;
using Incident.Application.Behaviors;
using Incident.Application.Commands.CreateIncident;
using Incident.Application.Commands.UpdateIncident;
using Incident.Application.Validators;
using Incident.Domain.Entities;
using Incident.Domain.Enums;
using Incident.Domain.Interfaces;
using Incident.Infrastructure.Persistence;
using Incident.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class IncidentTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    private ServiceProvider BuildServiceProvider()
    {
        var services = new ServiceCollection();
        services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
        services.AddScoped<IIncidentRepository, IncidentRepository>();
        services.AddMediatR(typeof(UpdateIncidentHandler).Assembly);
        services.AddValidatorsFromAssemblyContaining<UpdateIncidentValidator>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services.BuildServiceProvider();
    }


    [Fact]
    public async Task CreateIncident_ShouldCreateCorrectly()
    {
        var db = GetDbContext();
        var repo = new IncidentRepository(db);
        var handler = new CreateIncidentHandler(repo);

        var cmd = new CreateIncidentCommand(
            "Test",
            "Test description",
            Guid.NewGuid(),
            Guid.NewGuid()
        );

        var id = await handler.Handle(cmd, CancellationToken.None);

        Assert.NotEqual(Guid.Empty, id);
    }


    [Fact]
    public async Task UpdateIncident_Fail_InvalidStatus()
    {
        var provider = BuildServiceProvider();
        var mediator = provider.GetRequiredService<IMediator>();
        var db = provider.GetRequiredService<AppDbContext>();

        var incident = new IncidentEntity(
            "title",
            "desc",
            Guid.NewGuid(),
            Guid.NewGuid()
        );
        db.Incidents.Add(incident);
        await db.SaveChangesAsync();

        var invalidStatus = (IncidentStatus)50;

        var cmd = new UpdateIncidentCommand(
            incident.Id,
            "new title",
            "new description",
            invalidStatus
        );

        await Assert.ThrowsAsync<ValidationException>(() => mediator.Send(cmd));
    }


}
