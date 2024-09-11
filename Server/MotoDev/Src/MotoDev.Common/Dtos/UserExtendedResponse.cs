﻿namespace MotoDev.Common.Dtos
{
    public class UserExtendedResponse
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int? RoleId { get; set; }

        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string ImageUrl { get; set; }

    }
}