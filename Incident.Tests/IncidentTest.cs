using Incident.Application.Commands.CreateIncident;
using Incident.Domain.Entities;
using Incident.Domain.Interfaces;
using Incident.Infrastructure.Persistence;
using Incident.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class IncidentTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
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
}
