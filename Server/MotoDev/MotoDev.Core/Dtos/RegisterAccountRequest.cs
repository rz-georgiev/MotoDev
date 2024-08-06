using System.ComponentModel.DataAnnotations;

namespace MotoDev.Core.Dtos
{
    public class RegisterAccountRequest
    {
        //public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}