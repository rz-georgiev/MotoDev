using AutoMapper.Configuration.Annotations;

namespace MotoDev.Common.Dtos
{
    public class RepairShopRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string VatNumber { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}