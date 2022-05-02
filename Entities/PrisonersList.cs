namespace Entities;

//wrapper-class
public class PrisonersList
{
    public ICollection<Prisoner> Prisoners { get; set; }

    public PrisonersList()
    {
        Prisoners = new List<Prisoner>();
    }

    public PrisonersList(ICollection<Prisoner> prisoners)
    {
        this.Prisoners = prisoners;
    }
}