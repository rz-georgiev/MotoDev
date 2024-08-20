namespace MotoDev.Data.Models
{
    public class Model : BaseModel
    {
        public string Name { get; set; }
        
        public int BrandId { get; set; }

        public Brand Brand { get; set; }
    }
}