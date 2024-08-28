namespace MotoDev.Domain.Entities
{
    public class Repair : BaseModel
    {
        public int ClientCarId { get; set; }

        public int RepairTypeId { get; set; }

        public string Notes { get; set; }

        public int LastKilometers { get; set; }

        public decimal TotalPrice { get; set; }

        public ClientCar ClientCar { get; set; }

        public IEnumerable<ClientCarRepair> ClientRepairs { get; set; }

        
    }
}