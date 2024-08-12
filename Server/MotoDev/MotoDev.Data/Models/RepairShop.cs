namespace MotoDev.Data.Models
{
    public class RepairShop : BaseModel
    {
        public int OwnerUserId { get; set; }
        
        public string Name { get; set; }

        public User OwnerUser { get; set; }

        public IEnumerable<RepairShopUser> WorkingUsers { get; set; }

        public IEnumerable<Client> Clients { get; set; }
    }
}