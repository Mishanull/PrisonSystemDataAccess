using System.Text.Json;
using Entities;

namespace FileContext.Prisoners;

public class PrisonerFileContext
{
    private string prisonerFilePath = "prisoners.json";

    private ICollection<Prisoner>? prisoners;

    public ICollection<Prisoner> Prisoners
    {
        get
        {
            if (prisoners == null)
            {
                LoadData();
            }

            return prisoners;
        }
        set
        {
            prisoners = value;
        }
    }
    public PrisonerFileContext()
    {
        if (!File.Exists(prisonerFilePath))
        {
            Seed();
        }
    }
    private void Seed()
    {
        Prisoner[] pr = {
            new Prisoner
            {
                Id=1,
                FirstName = "Adam",
                LastName = "Smith",
                Ssn = 0321459584,
                CrimeCommitted = "Murder",
                Points = 120,
                Note = ""
            }
           
            
        };
        prisoners = pr.ToList();
        Task.FromResult(SaveChangesAsync());
    }
    public async Task SaveChangesAsync()
    {
        string serialize = JsonSerializer.Serialize(prisoners, new JsonSerializerOptions {
            WriteIndented = true,
            PropertyNameCaseInsensitive = false
        });
        await File.WriteAllTextAsync(prisonerFilePath,serialize);
        prisoners = null;
    }
    private void LoadData()
    {
        string content = File.ReadAllText(prisonerFilePath);
        prisoners = JsonSerializer.Deserialize<List<Entities.Prisoner>>(content);
    }
}