using Entities;

namespace Interfaces;

public interface IAlertService
{
    public Task AddAlertAsync(Alert alert);
    public Task<ICollection<Alert>> getAlertsAsync();
}