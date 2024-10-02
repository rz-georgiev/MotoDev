namespace MotoDev.Common.Dtos
{
    public class MechanicRepairResponse
    {
        public string CarImageUrl { get; set; }
        
        public string CarDescription { get; set; }

        public string OrderName { get; set; }

        public IEnumerable<MechanicRepairResponseDetail> Details { get; set; }

    }
}