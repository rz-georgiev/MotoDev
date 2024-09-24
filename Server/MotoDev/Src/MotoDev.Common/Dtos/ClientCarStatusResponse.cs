namespace MotoDev.Common.Dtos
{
    public class ClientCarStatusResponse
    {
        public string LicensePlateNumber { get; set; }

        public string VehicleName { get; set; }

        public IList<ClientCarStatusDetailResponse> Details { get; set; }
    }
}