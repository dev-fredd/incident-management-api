namespace Incident.Infrastructure.Persistence;


using Incident.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<IncidentEntity> Incidents { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IncidentEntity>().HasMany((incident) => incident.Comments).WithOne().HasForeignKey((comment) => comment.IncidentId);
        modelBuilder.Entity<CommentEntity>().Property(e => e.Id).ValueGeneratedNever();
        

        base.OnModelCreating(modelBuilder);

    }
}
