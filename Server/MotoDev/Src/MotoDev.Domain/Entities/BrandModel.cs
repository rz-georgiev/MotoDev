namespace MotoDev.Domain.Entities
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