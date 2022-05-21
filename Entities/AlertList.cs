namespace Entities;

public class AlertList
{
    public ICollection<Alert> Alerts { get; set; }

    public AlertList()
    {
        Alerts = new List<Alert>();
    }
    public AlertList(ICollection<Alert> alerts)
    {
        Alerts = alerts;
    }
}