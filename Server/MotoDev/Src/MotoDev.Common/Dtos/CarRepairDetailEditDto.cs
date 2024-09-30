namespace MotoDev.Common.Dtos
{
    public class CarRepairDetailEditDto
    {
        public int ClientCarRepairDetailId { get; set; }

        public int ClientCarRepairId { get; set; }

        public int RepairTypeId { get; set; }

        public int RepairStatusId { get; set; }

        public decimal Price { get; set; }

    }
}