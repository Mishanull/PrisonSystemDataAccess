using EfcData.Context;
using EfcData.DAO;
using Entities;
using Interfaces;

namespace TestProject;

public class PrisonerTesting
{
    private IPrisonerService _prisonerService = null!;
    private ISectorService _sectorService = null!;
    [OneTimeSetUp]
    public void Setup()
    {
        _prisonerService = new PrisonerDAO(new PrisonSystemContext());
        _sectorService = new SectorDAO(new PrisonSystemContext());
    }
    

    [Test]
    [Category("efc-prisoner_tests")]
    public async Task PrisonerFetchedByIdHasThatId_test()
    {
        long id;
        var inTestCreation = false;
        try
        { 
            id = (await _prisonerService.GetPrisonersAsync(1, 1)).First().Id;
        }
        catch (Exception)
        {
            await _prisonerService.CreatePrisonerAsync(await NewPrisoner());
            id = (await _prisonerService.GetPrisonersAsync(1, 1)).First().Id;
            inTestCreation = true;
        }
        
        Prisoner p = await _prisonerService.GetPrisonerByIdAsync(id);
        // Assert.AreEqual(p.Id, id);
        Assert.That(id, Is.EqualTo(p.Id));

        if (inTestCreation)
        {
            await _prisonerService.RemovePrisonerAsync(id);
        }
    }

    
    private async Task<Prisoner> NewPrisoner()
    {
        return new Prisoner
        {
            FirstName = "Firstname",
            LastName = "Lastname",
            Sector = (await _sectorService.GetSectorsAsync()).FirstOrDefault(),
            Ssn = 12324354,
            CrimeCommitted = "crime committed",
            EntryDate = DateTime.Now,
            ReleaseDate = DateTime.Now,
            Notes = new List<Note>()
        };
    }

    [Test]
    [Category("efc-prisoner_tests")]
    public async Task CreatePrisoner_test()
    {
        Prisoner p = await NewPrisoner();
        p = await _prisonerService.CreatePrisonerAsync(p);
        Assert.IsNotNull(p);
        await _prisonerService.RemovePrisonerAsync(p.Id);
    }
}