using Entities;

namespace Interfaces;

public interface IVisitService
{
    Task<Visit> CreateVisitAsync(Visit visit);
    Task<ICollection<Visit>> GetVisitsAsync();
    Task<Visit> GetVisitByAccessCodeAsync(string code);
    Task<Visit> UpdateVisitStatusAsync(long id, Visit.Status status);
}