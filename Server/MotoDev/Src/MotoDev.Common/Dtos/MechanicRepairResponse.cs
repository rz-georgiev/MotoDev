namespace MotoDev.Common.Dtos
{
    public class MechanicRepairResponse
    {
       
        public string OrderName { get; set; }

        public IEnumerable<MechanicRepairResponseDetail> Details { get; set; }

    }
}