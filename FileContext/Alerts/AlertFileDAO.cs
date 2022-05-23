using Entities;
using Interfaces;

namespace FileContext.Alerts;

public class AlertFileDAO : IAlertService
{
    private AlertFileContext _alertFileContext;

    public AlertFileDAO(AlertFileContext alertFileContext)
    {
        _alertFileContext = alertFileContext;
    }

    public async Task AddAlertAsync(Alert alert)
    {
        _alertFileContext.Alerts!.Add(alert);
        await _alertFileContext.SaveChangesAsync();
    }

    public async Task<ICollection<Alert>> getAlertsAsync()
    {
        return _alertFileContext.Alerts!;
    }
}