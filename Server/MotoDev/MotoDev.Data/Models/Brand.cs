namespace MotoDev.Data.Models
{
    public class Brand : BaseModel
    {
        public string Name { get; set; }
        
        public IEnumerable<Model> Models { get; set; }
    }
}