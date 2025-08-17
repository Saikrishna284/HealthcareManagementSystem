using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HealthCareManagementSystemWebAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Interaction> Interactions { get; set; } = new List<Interaction>();

    }
}