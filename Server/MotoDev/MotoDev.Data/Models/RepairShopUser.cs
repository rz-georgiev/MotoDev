namespace MotoDev.Data.Models
{
    public class RepairShopUser : BaseModel
    {
        public int RepairShopId { get; set; }
        
        public int UserId { get; set; }

        public RepairShop RepairShop { get; set; }

        public User User { get; set; }
    }
}