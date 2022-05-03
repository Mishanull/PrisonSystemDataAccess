using DAOInterfaces;
using EfcData.Context;
using Entities;

namespace EfcData.DAO;

public class UserDAO : IUserService
{
    private PrisonSystemContext _prisonSystemContext;

    public UserDAO(PrisonSystemContext prisonSystemContext)
    {
        _prisonSystemContext = prisonSystemContext;
    }

    public async Task<User> GetUserAsync(string username)
    {
        User? u=  _prisonSystemContext.Users.First(u => u.Username.Equals(username));
        return u;
    }
}