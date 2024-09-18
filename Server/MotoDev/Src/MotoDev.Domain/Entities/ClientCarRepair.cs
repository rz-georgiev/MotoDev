namespace MotoDev.Domain.Entities
{
    public class ClientCarRepair : BaseModel
    {
        public int ClientCarId { get; set; }

        public string Notes { get; set; }

        public int LastKilometers { get; set; }

        public int? RepairStatusId { get; set; }

        public RepairStatus? RepairStatus { get; set; }

        public ClientCar ClientCar { get; set; }

        public IEnumerable<ClientCarRepairDetail> ClientCarRepairsDetails { get; set; }
        
    }
}