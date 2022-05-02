namespace Entities;

//wrapper-class
public class PrisonersList
{
    public ICollection<Prisoner> prisoners { get; set; }

    public PrisonersList()
    {
        prisoners = new List<Prisoner>();
    }

    public PrisonersList(ICollection<Prisoner> prisoners)
    {
        this.prisoners = prisoners;
    }
}