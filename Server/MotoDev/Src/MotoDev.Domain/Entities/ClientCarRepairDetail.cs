namespace MotoDev.Domain.Entities
{
    public class ClientCarRepairDetail : BaseModel
    {
        public int ClientCarRepairId { get; set; }

        public int RepairTypeId { get; set; }

        public string Notes { get; set; }

        public decimal Price { get; set; }

        public ClientCarRepair ClientCarRepair { get; set; }

        public RepairType RepairType { get; set; }

    }
}