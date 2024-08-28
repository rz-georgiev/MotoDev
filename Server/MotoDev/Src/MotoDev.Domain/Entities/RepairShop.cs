namespace MotoDev.Domain.Entities
{
    public class RepairShop : BaseModel
    {
        public int OwnerUserId { get; set; }
        
        public string Name { get; set; }

        public User OwnerUser { get; set; }

        public IEnumerable<RepairShopUser> RepairShopUsers { get; set; }

        public IEnumerable<RepairShopClient> RepairShopClients { get; set; }
    }
}