namespace MotoDev.Domain.Entities
{
    public class RepairShopClient
    {
        public int Id { get; set; }

        public int RepairShopId { get; set; }

        public int ClientId { get; set; }

        public RepairShop RepairShop { get; set; }

        public Client Client { get; set; }
    }
}