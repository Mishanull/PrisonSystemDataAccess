using Entities;

namespace Interfaces;

public interface IAlertService
{
    Task AddAlertAsync(Alert alert);
    Task<ICollection<Alert>> getAlertsAsync();
    Task<ICollection<Alert>> getAlertsAsync(int pageNumber, int pageSize);
    Task<List<int>> GetAlertsTodayAsync();
}