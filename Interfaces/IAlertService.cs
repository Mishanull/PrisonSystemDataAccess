using Entities;

namespace Interfaces;

public interface IAlertService
{
    public Task AddAlertAsync(Alert alert);
    public Task<ICollection<Alert>> getAlertsAsync();
    Task<ICollection<Alert>> getAlertsAsync(int pageNumber, int pageSize);
}