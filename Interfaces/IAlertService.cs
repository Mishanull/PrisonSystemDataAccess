using Entities;

namespace Interfaces;

public interface IAlertService
{
    Task AddAlertAsync(Alert alert);
    Task<ICollection<Alert>> GetAlertsAsync(int pageNumber, int pageSize);
    Task<ICollection<Alert>> GetAlertsTodayAsync();
}