using System.Dynamic;

namespace MotoDev.Core.Dtos
{
    public class UserResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string RepairShop { get; set; }
        
        public string Position { get; set; }
    }
}