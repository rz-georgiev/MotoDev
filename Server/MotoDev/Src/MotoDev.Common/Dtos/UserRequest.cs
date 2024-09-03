namespace MotoDev.Common.Dtos
{
    public class UserRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        
        public string Username { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public int RepairShopId { get; set; }

        public int RoleId { get; set; }
    }
}