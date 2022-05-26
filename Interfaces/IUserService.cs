using System.Reflection.Metadata;
using Entities;

namespace DAOInterfaces;

public interface IUserService
{
    Task<User> GetUserAsync(string username);
}