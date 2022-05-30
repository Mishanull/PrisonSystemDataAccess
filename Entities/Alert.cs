using System.Text.Json.Serialization;

namespace Entities;

public class Alert
{
    public DateTime DateTime { get; set; }
    public String Text { get; set; }
    public bool[] Sectors { get; set; } = new bool[3];
    
    public int DurationInMinutes { get; set; }
   
}