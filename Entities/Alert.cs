namespace Entities;

public class Alert
{
    public DateTime DateTime { get; set; }
    public String Text { get; set; }

    public PriorityE Priority { get; set; }

    public enum PriorityE
    {
        Low,
        Medium,
        High
    }
}