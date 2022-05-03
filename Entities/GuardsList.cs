namespace Entities;

public class GuardsList
{
    public ICollection<Guard> guards { get; set; }

    public GuardsList()
    {
        guards = new List<Guard>();
    }

    public GuardsList(ICollection<Guard> guards)
    {
        this.guards = guards;
    } 
}