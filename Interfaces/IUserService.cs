using System.Reflection.Metadata;
using Entities;

namespace DAOInterfaces;

public interface IUserService
{
    public Task<User> GetUserAsync(string username);
}