using Incident.Domain.Entities;
using Incident.Domain.Interfaces;
using Incident.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Incident.Infrastructure.Repositories;

public class IncidentRepository : IIncidentRepository
{
    private readonly AppDbContext _context;

    public IncidentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(IncidentEntity incident)
        => await _context.Incidents.AddAsync(incident);

    public async Task<List<IncidentEntity>> GetAllAsync()
        => await _context.Incidents.Include(i => i.Comments).ToListAsync();

    public async Task<IncidentEntity> GetByIdAsync(Guid id)
        => await _context.Incidents
                .Include(i => i.Comments)
                .FirstOrDefaultAsync(i => i.Id == id);
    public Task UpdateAsync(IncidentEntity incident)
    {

        _context.Incidents.Update(incident);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(IncidentEntity incident)
    {
        _context.Incidents.Remove(incident);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync()
        => _context.SaveChangesAsync();
}
