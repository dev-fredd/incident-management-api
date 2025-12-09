
namespace Incident.Domain.Interfaces;

using Incident.Domain.Entities;

public interface IIncidentRepository
{
    Task<IncidentEntity> GetByIdAsync(Guid id);
    Task<List<IncidentEntity>> GetAllAsync();
    Task AddAsync(IncidentEntity incident);
    Task UpdateAsync(IncidentEntity incident);
    Task DeleteAsync(IncidentEntity incident);
    Task SaveChangesAsync();
}
