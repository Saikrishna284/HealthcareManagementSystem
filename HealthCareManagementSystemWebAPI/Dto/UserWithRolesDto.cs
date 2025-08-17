using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareManagementSystemWebAPI.Dto
{
    public class UserWithRolesDto
    {
        public required string Id { get; set; }
        public required string UserName { get; set; }
        public string? Email { get; set; }
         public required List<string> Roles { get; set; }
    }
}