namespace MotoDev.Data.Models
{
    public class UserRepairShop
    {
        public int Id { get; set; }

        public int RepairShopId { get; set; }

        public int UserId { get; set; }

        public RepairShop RepairShop { get; set; }

        public User User { get; set; }
    }
}