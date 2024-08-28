namespace MotoDev.Domain.Entities
{
    public class Client : BaseModel
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public IEnumerable<ClientCar> ClientCars { get; set; }
        
    }
}