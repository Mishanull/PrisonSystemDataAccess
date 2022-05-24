using DAOInterfaces;
using Entities;

namespace FileContext.Guards;

public class GuardFileDAO : IGuardService
{
    private GuardFileContext _guardFileContext;

    public GuardFileDAO(GuardFileContext guardFileContext)
    {
        _guardFileContext = guardFileContext;
    }

    public async Task<Guard> CreateGuardAsync(Guard guard)
    {
        long largestId = -1;
        if (_guardFileContext.Guards.Any())
        {
            largestId = _guardFileContext.Guards.Max(p => p.Id);
        }

        guard.Id = ++largestId;
        _guardFileContext.Guards.Add(guard);
        await _guardFileContext.SaveChangesAsync();
        return guard;
    }

    public async Task<Guard> GetGuardByIdAsync(long id)
    {
        Guard? temp = _guardFileContext.Guards.First(g => g.Id == id);
        return temp;
    }

    public async Task RemoveGuardAsync(long id)
    {
        _guardFileContext.Guards?.Remove(_guardFileContext.Guards.First((p => p.Id == id)));
        await _guardFileContext.SaveChangesAsync();
    }

    public async Task<Guard> UpdateGuardAsync(Guard guard)
    {
        Guard? guardToUpdate = GetGuardByIdAsync(guard.Id).Result;
        guardToUpdate.FirstName = guard.FirstName;
        guardToUpdate.LastName = guard.LastName;
        guardToUpdate.Email = guard.Email;
        guardToUpdate.PhoneNumber = guard.PhoneNumber;
        guardToUpdate.Role = guard.Role;
        guardToUpdate.Password = guard.Password;
        await _guardFileContext.SaveChangesAsync();
        return guardToUpdate;
    }

    public async Task<ICollection<Guard>> GetGuards()
    {
        return _guardFileContext.Guards.ToList();
    }

    public Task<Sector> GetGuardBySector(long id)
    {
        throw new NotImplementedException();
    }
}