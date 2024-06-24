namespace MotoDev.Core.Dtos
{
    public class ResetPasswordRequest
    {
        public string ResetPasswordToken { get; set; }

        public string Password { get; set; }
    }
}
