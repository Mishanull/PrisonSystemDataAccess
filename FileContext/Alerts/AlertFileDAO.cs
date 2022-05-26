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

    public async Task<ICollection<Alert>> getAlertsAsync(int pageNumber, int pageSize)
    {
        return _alertFileContext.Alerts!.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
    }
    
    // Number of alerts the last 24hs.
    public async Task<List<int>> GetAlertsTodayAsync()
    {
        ICollection<Alert> alerts = await getAlertsAsync();
        int alerts24Hs = alerts.Count(alert => alert.DateTime > DateTime.Now - TimeSpan.FromHours(24));
        var numAlertsToday = new List<int>
        {
            alerts24Hs
        };
        return numAlertsToday;
    }
}