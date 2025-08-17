using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareManagementSystemWebAPI.Dto
{
    public class UserDto
    {
        public required string Id { get; set; }
        public required string UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}