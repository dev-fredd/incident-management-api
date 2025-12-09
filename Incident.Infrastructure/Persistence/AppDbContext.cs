namespace Incident.Infrastructure.Persistence;


using Incident.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<IncidentEntity> Incidents { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
}
