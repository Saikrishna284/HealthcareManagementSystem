using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareManagementSystemWebAPI.Dto;

namespace HealthCareManagementSystemWebAPI.Interface
{
    public interface IInteractionService
    {
        Task<IEnumerable<InteractionsDto>> GetInteractionsByPatientId(int id);

        Task AddInteraction(InteractionsDto interaction);

        Task UpdateIntearction(UpdateInteractionsDto interaction);


    }
}