namespace MotoDev.Common.Dtos
{
    public class ClientCarStatusDetailResponse
    {
        public int StatusId { get; set; }

        public string StatusName { get; set; }

        public string RepairName { get; set; }

        public DateTime? RepairStartDateTime { get; set; }

        public DateTime? RepairEndDateTime { get; set; }
    }
}
