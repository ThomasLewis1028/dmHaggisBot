namespace SWNUniverseGenerator.Models;

public class PoiOccupiedBy : BaseEntity
{
    public string TypeId { get; set; }
    
    public string OccupiedBy { get; set; }
}