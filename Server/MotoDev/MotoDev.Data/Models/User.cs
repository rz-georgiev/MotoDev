namespace MotoDev.Data.Models
{
    public class User : BaseModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string? ResetPasswordToken { get; set; }

        public bool IsActive { get; set; } = false;
        
        public IEnumerable<UserRole> UserRoles { get; set; }

        public IEnumerable<RepairShop>? RepairShops { get; set; }
    }
}