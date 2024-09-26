namespace MotoDev.Domain.Entities
{
    public class CarRepairStatus : BaseModel
    {
        public string Name { get; set; }
        
        public IEnumerable<Model> Models { get; set; }
    }
}