using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareManagementSystemWebAPI.Models
{
    public class AuthModel
    {
        public class LoginModel
        {
            public required string Username { get; set; }
            public required string Password { get; set; }
        }

        public class RegisterModel
        {
            public required string Username { get; set; }
            public required string Email { get; set; }
            public required string Password { get; set; }
            public required string Role { get; set; }
        }

        public class AuthResponse
        {
            public required string Token { get; set; }
            public required string Username { get; set; }
            public required string Email { get; set; }
            public required List<string> Roles { get; set; }
            public DateTime Expiration { get; set; }
        }
    }
}