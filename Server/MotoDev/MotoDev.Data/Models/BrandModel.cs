namespace MotoDev.Data.Models
{
    public class BrandModel
    {
        public int Id { get; set; }

        public int BrandId { get; set; }

        public int ModelId { get; set; }

        public Brand Brand { get; set; }

        public Model Model { get; set; }
    }
}