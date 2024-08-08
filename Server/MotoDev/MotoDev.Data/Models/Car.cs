namespace MotoDev.Data.Models
{
    public class Car : BaseModel
    {
        public int BrandId { get; set; }

        public int ModelId { get; set; }

        public int HorsePowers { get; set; }

        public int EuroStandard { get; set; }

        public int Year { get; set; }

        public int CarTypeId { get; set; }

        public int EngineTypeId { get; set; }

        public int TransmissionTypeId { get; set; }

        public string Color { get; set; }

        public Brand Brand { get; set; }

        public Model Model { get; set; }

        public CarType CarType { get; set; }

        public EngineType EngineType { get; set; }

        public TransmissionType TransmissionType { get; set; }
    }
}