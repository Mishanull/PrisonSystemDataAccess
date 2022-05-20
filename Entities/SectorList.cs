namespace Entities;

public class SectorList
{
    public ICollection<Sector> Sectors { get; set; }

    public SectorList()
    {
        Sectors = new List<Sector>();
    }

    public SectorList(ICollection<Sector> sectors)
    {
        Sectors = sectors;
    }
}