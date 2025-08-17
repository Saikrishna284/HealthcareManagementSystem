using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareManagementSystemWebAPI.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public List<Interaction> Interactions { get; set; } = new List<Interaction>();
    }
}