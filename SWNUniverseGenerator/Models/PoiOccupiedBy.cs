namespace SWNUniverseGenerator.Models;

public class PoiOccupiedBy : BaseEntity
{
    public string Type { get; set; }
    
    public string OccupiedBy { get; set; }
}