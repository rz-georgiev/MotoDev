﻿namespace MotoDev.Domain.Entities
{
    public class User : BaseModel
    {
        public string Username { get; set; }
        
        public string Password { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Email { get; set; }
        
        public string? PhoneNumber { get; set; }

        public string? ResetPasswordToken { get; set; }

        public bool IsActive { get; set; } = false;
        
        public IEnumerable<UserRole> UserRoles { get; set; }
        
    }
}