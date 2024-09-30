namespace MotoDev.Common.Dtos
{
    public class CarRepairDetailListingResponse
    {
        public int ClientCarRepairDetailId { get; set; }

        public string ClientName { get; set; }

        public string LicensePlateNumber { get; set; }

        public string RepairTypeName { get; set; }

        public decimal Price { get; set; }

        public string Status { get; set; }

    }
}