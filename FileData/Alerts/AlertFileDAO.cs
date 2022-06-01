using Entities;
using Interfaces;

namespace FileData.Alerts;

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

    public async Task<ICollection<Alert>> GetAlertsAsync(int pageNumber, int pageSize)
    {
        return _alertFileContext.Alerts!.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
    }
    
    public async Task<ICollection<Alert>> GetAlertsTodayAsync()
    {
        ICollection<Alert> alerts =  _alertFileContext!.Alerts!.Where(alert => alert.DateTime > DateTime.Now - TimeSpan.FromHours(24)).ToList() ;
        
        return alerts;
    }
}