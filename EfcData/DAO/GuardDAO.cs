using DAOInterfaces;
using EfcData.Context;
using Entities;

namespace EfcData.DAO;

public class GuardDAO : IGuardService
{
    private PrisonSystemContext _prisonSystemContext;

    public GuardDAO(PrisonSystemContext prisonSystemContext)
    {
        _prisonSystemContext = prisonSystemContext;
    }
    
    public async Task<Guard> CreateGuardAsync(Guard guard)
    {
        long largestId = -1;
        if (_prisonSystemContext.Guards.Any())
        {
            largestId = _prisonSystemContext.Guards.Max(p => p.Id);
        }

        guard.Id = ++largestId;
        _prisonSystemContext.Guards.Add(guard);
        await _prisonSystemContext.SaveChangesAsync();
        return guard;
    }

    public async Task<Guard> GetGuardByIdAsync(long id)
    {
        Guard? temp = _prisonSystemContext.Guards.First(g => g.Id == id);
        return temp;
    }

    public async Task RemoveGuardAsync(long id)
    {
        _prisonSystemContext.Guards?.Remove(_prisonSystemContext.Guards.First((p => p.Id == id)));
        await _prisonSystemContext.SaveChangesAsync();
    }

    public async Task<Guard> UpdateGuardAsync(Guard? guard)
    {
        Guard? guardToUpdate = GetGuardByIdAsync(guard.Id).Result;
        guardToUpdate.FirstName = guard.FirstName;
        guardToUpdate.LastName = guard.LastName;
        guardToUpdate.Email = guard.Email;
        guardToUpdate.PhoneNumber = guard.PhoneNumber;
        guardToUpdate.Role = guard.Role;
        guardToUpdate.Password = guard.Password;
        await _prisonSystemContext.SaveChangesAsync();
        return guardToUpdate;
    }

    public async Task<ICollection<Guard>> GetGuards()
    {
        return _prisonSystemContext.Guards.ToList();
    }
}