using System.Text.Json;
using Entities;

namespace FileContext.Users;

public class UserFileContext
{
    private string userFilePath = "users.json";

    private ICollection<User> users;

    public ICollection<User> Users
    {
        get
        {
            if (users == null)
            {
                LoadData();
            }

            return users;
        }
        set
        {
            users = value;
        }
    }
    public UserFileContext()
    {
        if (!File.Exists(userFilePath))
        {
            Seed();
        }
    }
    private void Seed()
    {
        User[] ts = {
            new User {
                Id=1,
                Username = "mike",
                FirstName = "Mihai",
                LastName = "Avram",
                Password = "password"
                
            },
           
            
        };
        users = ts.ToList();
        SaveChanges();
    }
    public void SaveChanges()
    {
        string serialize = JsonSerializer.Serialize(Users);
        File.WriteAllText(userFilePath,serialize);
        users = null;
    }
    private void LoadData()
    {
        string content = File.ReadAllText(userFilePath);
        Users = JsonSerializer.Deserialize<List<User>>(content);
    }
}