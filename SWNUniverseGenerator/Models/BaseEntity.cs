namespace SWNUniverseGenerator.Models
{
    public class BaseEntity : IEntity
    {
        protected BaseEntity()
        {
            Id = this.GenerateId();
        }
        
        public string Id { get; set; }
    }
}