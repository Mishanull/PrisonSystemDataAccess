namespace Entities;

public class GuardsList
{
    public ICollection<Guard> Guards { get; set; }

    public GuardsList()
    {
        Guards = new List<Guard>();
    }

    public GuardsList(ICollection<Guard> guards)
    {
        this.Guards = guards;
    } 
}