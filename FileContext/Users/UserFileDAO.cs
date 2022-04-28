using DAOInterfaces;
using Entities;

namespace FileContext.Users;

public class UserFileDAO: IUserService
{
    private UserFileContext _fileContext;

    public UserFileDAO(UserFileContext fileContext)
    {
        _fileContext = fileContext;
    }

    public async Task<User?> GetUserAsync(string username)
    {
        User? u=  _fileContext.Users.First(u => u.Username.Equals(username));
        return u;
    }
}