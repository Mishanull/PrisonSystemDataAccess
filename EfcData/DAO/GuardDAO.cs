using DAOInterfaces;
using EfcData.Context;
using Entities;
using Microsoft.EntityFrameworkCore;

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
        Console.WriteLine(temp);
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

    public async Task<Sector> GetGuardBySector(long id)
    {
        Guard g = _prisonSystemContext.Guards.First(g => g.Id == id);
        WorkShift shift = _prisonSystemContext.WorkShifts.First(workShift => workShift!.Guards!.Contains(g));
        _prisonSystemContext.Entry(shift).Reference(w => w.Sector).Load();
        
        return shift.Sector!;
    }
    
    // List of guards working today in specific sector.
    public async Task<ICollection<Guard>> GetGuardsPerSectTodayAsync(long sectorId)
    {
        ICollection<WorkShift> workShifts = await GetWorkShiftsAsync();
        return workShifts.Where(workShift => 
            workShift.Guards != null 
            && workShift.Sector!.Id == sectorId 
            && workShift.DaysOfWeek!.Contains(DateTime.Now.DayOfWeek.ToString()))
            .SelectMany(workShift => workShift.Guards!).ToList();
    }
    
    // List of 3 int, with the amount of guards per sector and the total. [sect1, sect2, sect3, total]
    // ((sect1 + sect2 + sect3) - total) = guards without shift
    public async Task<List<int>> GetNumGuardsPerSectAsync()
    {
        ICollection<WorkShift> workShifts = await GetWorkShiftsAsync();
        var numGuards = GetGuards().Result.Count;
        int sect1 = 0, sect2 = 0, sect3 = 0;
        foreach (var workShift in workShifts)
        {
            if (workShift.Guards == null) continue;
            switch (workShift.Sector!.Id)
            {
                case 1:
                    sect1 += workShift.Guards.Count;
                    break;
                case 2:
                    sect2 += workShift.Guards.Count;
                    break;
                case 3:
                    sect3 += workShift.Guards.Count;
                    break;
            }
        }
        var numGuardPerSect = new List<int>
        {
            sect1,
            sect2,
            sect3,
            numGuards
        };
        return numGuardPerSect;
    }

    public async Task<List<int>> GetNumGuardsPerSectTodayAsync()
    {
        ICollection<WorkShift> workShifts = await GetWorkShiftsAsync();
        int sect1 = 0, sect2 = 0, sect3 = 0;
        foreach (var workShift in workShifts)
        {
            if (workShift.Guards != null
                && workShift.DaysOfWeek!.Contains(DateTime.Now.DayOfWeek.ToString()))
            {
                switch (workShift.Sector!.Id)
                {
                    case 1:
                        sect1 += workShift.Guards.Count;
                        break;
                    case 2:
                        sect2 += workShift.Guards.Count;
                        break;
                    case 3:
                        sect3 += workShift.Guards.Count;
                        break;
                }
            }
        }
        var numGuardPerSectToday = new List<int>
            {
                sect1,
                sect2,
                sect3
            };
        return numGuardPerSectToday;
    }
    
    private async Task<ICollection<WorkShift>> GetWorkShiftsAsync()
    {
        ICollection<WorkShift> shifts = await _prisonSystemContext.WorkShifts
            .Include(shift => shift.Guards)
            .Include(sector => sector.Sector)
            .ToListAsync();
        Console.WriteLine(shifts);
        return shifts;
    }
}