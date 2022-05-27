using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Entities;

namespace FileContext.Alerts;

public class AlertFileContext
{
    private string AlertFilePath = "alerts.json";

    private ICollection<Alert>? _alerts;

    [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
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
        if (!File.Exists(AlertFilePath))
        {
            Seed();
        }
    }
    private void Seed()
    {
        Alert[] a = {
            new Alert {
                DateTime = DateTime.Now,
                Priority = Priority.Medium,
                Text = "alert test -seed",
                Sectors = new []{true, true, true}
            }
        };
        _alerts = a.ToList();
        Task.FromResult(SaveChangesAsync());
    }
    [SuppressMessage("ReSharper.DPA", "DPA0003: Excessive memory allocations in LOH")]
    public async Task SaveChangesAsync()
    {
        string serialize = JsonSerializer.Serialize(Alerts, new JsonSerializerOptions {
            WriteIndented = true,
            PropertyNameCaseInsensitive = false
        });
        await File.WriteAllTextAsync(AlertFilePath,serialize);
        _alerts = null;
    }
    [SuppressMessage("ReSharper.DPA", "DPA0003: Excessive memory allocations in LOH")]
    [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH", MessageId = "type: System.Char[]")]
    private void LoadData()
    {
        string content = File.ReadAllText(AlertFilePath);
        Alerts = JsonSerializer.Deserialize<List<Alert>>(content);
    }
}