using System.Text.Json;
using Entities;

namespace FileContext.Guards;

public class GuardFileContext
{
    private string guardsFilePath = "guards.json";

    private ICollection<Guard> guards;

    public ICollection<Guard> Guards
    {
        get
        {
            if (guards == null)
            {
                LoadData();
            }

            return guards;
        }
        set
        {
            guards = value;
        }
    }
    public GuardFileContext()
    {
        if (!File.Exists(guardsFilePath))
        {
            Seed();
        }
    }
    private void Seed()
    {
        Guard[] help = {
            new Guard {
                Id=1,
                Username = "LadyFingers",
                Password = "ilovemyjob123",
                FirstName = "Pampalini",
                LastName = "Hunter",
                Email = "lonelyguard@hotmail.com",
                PhoneNumber = "+4565984532"
            },
        };
        guards = help.ToList();
        SaveChanges();
    }
    public void SaveChanges()
    {
        string serialize = JsonSerializer.Serialize(Guards);
        File.WriteAllText(guardsFilePath,serialize);
        guards = null;
    }
    private void LoadData()
    {
        string content = File.ReadAllText(guardsFilePath);
        Guards = JsonSerializer.Deserialize<List<Guard>>(content);
    }
}