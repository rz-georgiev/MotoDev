using System.Diagnostics.CodeAnalysis;

namespace MotoDev.Domain.Entities
{
    public class ClientCarRepairDetail : BaseModel
    {
        public int ClientCarRepairId { get; set; }

        public int RepairTypeId { get; set; }

        public string Notes { get; set; }

        public decimal Price { get; set; }

        public DateTime? RepairStartDateTime { get; set; }

        public DateTime? RepairEndDateTime { get; set; }

        public int RepairStatusId { get; set; }

        public RepairStatus RepairStatus { get; set; }

        public ClientCarRepair ClientCarRepair { get; set; }

        public RepairType RepairType { get; set; }

    }
}