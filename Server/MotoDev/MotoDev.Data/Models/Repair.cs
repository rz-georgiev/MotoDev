namespace MotoDev.Data.Models
{
    public class Repair : BaseModel
    {
        public int ClientCarId { get; set; }

        public int RepairTypeId { get; set; }

        public string Notes { get; set; }

        public int LastKilometers { get; set; }

        public decimal Price { get; set; }

        public RepairType RepairType { get; set; }

        public ClientCar ClientCar { get; set; }
    }
}