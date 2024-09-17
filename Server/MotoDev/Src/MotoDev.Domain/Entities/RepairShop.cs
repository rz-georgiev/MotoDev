namespace MotoDev.Domain.Entities
{
    public class RepairShop : BaseModel
    {
        public int OwnerUserId { get; set; }
        
        public string Name { get; set; }

        public string City { get; set; }
        
        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string VatNumber { get; set; }

        public bool IsActive { get; set; }

        public User OwnerUser { get; set; }

        public IEnumerable<RepairShopUser> RepairShopUsers { get; set; }

        public IEnumerable<RepairShopClient> RepairShopClients { get; set; }

    }
}