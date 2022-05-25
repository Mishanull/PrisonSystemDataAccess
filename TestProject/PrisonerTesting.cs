using EfcData.Context;
using EfcData.DAO;
using Entities;
using Interfaces;

namespace TestProject;

public class PrisonerTesting
{
    private IPrisonerService _prisonerService = null!;
    private ISectorService _sectorService = null!;
    [SetUp]
    public void Setup()
    {
        _prisonerService = new PrisonerDAO(new PrisonSystemContext());
        _sectorService = new SectorDAO(new PrisonSystemContext());
    }

    
    
    [Test]
    [OneTimeSetUp]
    public async Task CreatePrisoner()
    {
        var p = NewPrisoner();
    }
    [Category("efc-prisoner_tests")]
    public async Task PrisonerFetchedByIdHasThatId_test()
    {
        long id;
        try
        {
            id = (await _prisonerService.GetPrisonersAsync(1, 1)).First().Id;
        }
        catch (Exception)
        {
            await _prisonerService.CreatePrisonerAsync(await NewPrisoner());
            id = (await _prisonerService.GetPrisonersAsync(1, 1)).First().Id;
        }
        
        Prisoner p = await _prisonerService.GetPrisonerByIdAsync(id);
        // Assert.AreEqual(p.Id, id);
        Assert.That(id, Is.EqualTo(p.Id));
    }

    
    private async Task<Prisoner> NewPrisoner()
    {
        var p = new Prisoner
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
        return p;
    }

    [Test]
    [Category("efc-prisoner_tests")]
    public async Task CreatePrisoner_test()
    {
        Prisoner p = await NewPrisoner();
        p = await _prisonerService.CreatePrisonerAsync(p);
        Assert.IsNotNull(p);
    }
}