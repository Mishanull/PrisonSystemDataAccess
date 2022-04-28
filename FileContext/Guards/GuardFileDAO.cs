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

    public async Task<Guard> CreateGuard(Guard guard)
    {
        _guardFileContext.Guards.Add(guard);
         _guardFileContext.SaveChanges();
        return guard;
    }

    public async Task<Guard> GetGuardById(long id)
    {
        Guard? temp = _guardFileContext.Guards.First(g => g.Id == id);
        return temp;
    }
}