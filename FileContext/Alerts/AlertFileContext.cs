using System.Text.Json;
using Entities;

namespace FileContext.Alerts;

public class AlertFileContext
{
    private string alertFilePath = "alerts.json";

    private ICollection<Alert>? _alerts;

    public ICollection<Alert>? Alerts
    {
        get
        {
            if (_alerts == null)
            {
                LoadData();
            }

            return _alerts;
        }
        set
        {
            _alerts = value;
        }
    }
    public AlertFileContext()
    {
        if (!File.Exists(alertFilePath))
        {
            Seed();
        }
    }
    private void Seed()
    {
        Alert[] a = {
            new Alert {
                dateTime = DateTime.Now,
                priority = Alert.Priority.Medium,
                text = "alert test -seed"
            }
        };
        _alerts = a.ToList();
        Task.FromResult(SaveChangesAsync());
    }
    public async Task SaveChangesAsync()
    {
        string serialize = JsonSerializer.Serialize(Alerts, new JsonSerializerOptions {
            WriteIndented = true,
            PropertyNameCaseInsensitive = false
        });
        await File.WriteAllTextAsync(alertFilePath,serialize);
        _alerts = null;
    }
    private void LoadData()
    {
        string content = File.ReadAllText(alertFilePath);
        Alerts = JsonSerializer.Deserialize<List<Alert>>(content);
    }
}