﻿namespace MotoDev.Data.Models
{
    public class Client : BaseModel
    {
        public string FirstName { get; set; }
    
        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public IEnumerable<ClientCar> ClientCars{ get; set; }
        
    }
}