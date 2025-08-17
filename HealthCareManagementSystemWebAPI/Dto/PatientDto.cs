using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareManagementSystemWebAPI.Models;

namespace HealthCareManagementSystemWebAPI.Dto
{

    public class GetPatientDto
    {
        public int PatientId { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public List<InteractionsDto> Interactions { get; set; } = new List<InteractionsDto>();

    }
    public class AddPatientDto
    {
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        
    }

    public class UpdatePatientDto
    {
        public int PatientId { get; set; }

        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
       
    }
}