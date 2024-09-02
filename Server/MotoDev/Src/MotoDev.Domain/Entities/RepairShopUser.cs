namespace MotoDev.Domain.Entities
{
    public class RepairShopUser : BaseModel
    {
        public int RepairShopId { get; set; }
        
        public int UserId { get; set; }

        public bool IsActive { get; set; } = true;

        public RepairShop RepairShop { get; set; }

        public User User { get; set; }
    }
}