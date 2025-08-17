using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareManagementSystemWebAPI.Models;

namespace HealthCareManagementSystemWebAPI.Dto
{
    public class InteractionsDto
    {
        public int InteractionId { get; set; }
        public int PatientId { get; set; }
        public required string UserId { get; set; }
        public required string Description { get; set; }
        public DateTime Date { get; set; }
        //public required Patient Patient { get; set; }
        //public required ApplicationUser User { get; set; }
    }


    public class UpdateInteractionsDto
    {
        public int InteractionId { get; set; }
        public int PatientId { get; set; }
        public required string UserId { get; set; }
        public required string Description { get; set; }
        public DateTime Date { get; set; }

    }
}