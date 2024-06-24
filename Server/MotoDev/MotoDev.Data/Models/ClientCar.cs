namespace EngineExpert.Data.Models
{
    public class ClientCar : BaseModel
    {
        public int ClientId { get; set; }

        public int CarId { get; set; }

        public Client Client { get; set; }

        public Car Car { get; set; }

        public IEnumerable<Repair> Repairs { get; set; }
    }
}