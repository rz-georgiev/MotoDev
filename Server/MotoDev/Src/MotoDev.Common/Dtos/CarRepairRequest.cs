namespace MotoDev.Common.Dtos
{
    public class CarRepairRequest
    {
        public int? CarRepairId { get; set; }

        public int ClientCarId { get; set; }

        public int MechanicUserId { get; set; }

    }
}