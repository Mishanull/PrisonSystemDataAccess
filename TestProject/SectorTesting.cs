using EfcData.Context;
using EfcData.DAO;
using Entities;
using Interfaces;

namespace TestProject;

public class SectorTesting
{
    private ISectorService _sectorService = null!;
    [SetUp]
    public void Setup()
    {
        _sectorService = new SectorDAO(new PrisonSystemContext());
    }

    [Test]
    [Category("efc-sector_tests")]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public async Task Sectors_test(long id)
    {
        Sector? s;
        try
        {
            s = await _sectorService.GetSectorByIdAsync(id);
        }
        catch (Exception e)
        {
            s = null;
        }
        Assert.That(s, Is.Not.Null, $"sector S:{id} does not exist");
    }
}