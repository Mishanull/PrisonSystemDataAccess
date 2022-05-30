using Entities;
using Entities.Enums;

namespace Interfaces;

public interface IVisitService
{
    Task<Visit> CreateVisitAsync(Visit visit);
    Task<Visit> GetVisitByAccessCodeAsync(string code);
    Task<Visit> UpdateVisitStatusAsync(long id, Status status, string accessCode);
    Task<ICollection<Visit>> GetVisitsAsync(int pageNumber, int pageSize);
    Task<ICollection<Visit>> GetVisitsTodayAsync();
    Task<ICollection<Visit>> GetVisitsPendingAsync();
}