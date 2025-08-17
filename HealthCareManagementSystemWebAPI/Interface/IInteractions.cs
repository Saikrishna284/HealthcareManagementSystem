using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareManagementSystemWebAPI.Models;

namespace HealthCareManagementSystemWebAPI.Interface
{
    public interface IInteractions
    {
        Task<IEnumerable<Interaction>> GetInteractionsByPatientId(int id);

        Task AddInteraction(Interaction interaction);

        Task UpdateInteractions(Interaction  interaction);
    }
}