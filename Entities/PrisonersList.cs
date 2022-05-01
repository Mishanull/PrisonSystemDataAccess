namespace Entities;

//wrapper-class
public class PrisonersList
{
    private ICollection<Prisoner> prisonersList { get; set; }

    public PrisonersList()
    {
        prisonersList = new List<Prisoner>();
    }

    public PrisonersList(ICollection<Prisoner> prisonersList)
    {
        this.prisonersList = prisonersList;
    }
}