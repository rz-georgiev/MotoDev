namespace MotoDev.Domain.Entities
{
    public class ClientCarRepair : BaseModel
    {
        public int RepairId { get; set; }

        public int RepairTypeId { get; set; }

        public string Notes { get; set; }

        public decimal Price { get; set; }

        public Repair Repair { get; set; }

        public RepairType RepairType { get; set; }
    }
}