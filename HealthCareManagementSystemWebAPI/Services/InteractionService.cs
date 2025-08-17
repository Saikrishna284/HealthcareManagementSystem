using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HealthCareManagementSystemWebAPI.Dto;
using HealthCareManagementSystemWebAPI.Interface;
using HealthCareManagementSystemWebAPI.Models;

namespace HealthCareManagementSystemWebAPI.Services
{
    public class InteractionService : IInteractionService
    {

        private readonly IInteractions _repo;
        private readonly IMapper _mapper;

        public InteractionService(IInteractions repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task AddInteraction(InteractionsDto interaction)
        {
            try
            {
                var interactionMap = _mapper.Map<Interaction>(interaction);
                await _repo.AddInteraction(interactionMap);
            }
            catch(Exception)
            {
                throw new Exception("Error occurred while adding new interaction.");
            }
        }

        public async Task<IEnumerable<InteractionsDto>> GetInteractionsByPatientId(int id)
        {
            try
            {
                var interactions = await _repo.GetInteractionsByPatientId(id);
                return _mapper.Map<IEnumerable<InteractionsDto>>(interactions);
            }
            catch(KeyNotFoundException)
            {
                throw;
            }
            catch(Exception)
            {
                throw new Exception("Error occurred while fetching interactions.");
            }
        }

        public async Task UpdateIntearction(UpdateInteractionsDto interaction)
        {
            try
            {
                var interactionMap = _mapper.Map<Interaction>(interaction);
                await _repo.UpdateInteractions(interactionMap);
            }
            catch(Exception ex)
            {
               throw new Exception("Error occurred while Updating patients in service layer.", ex);
            }
        }
    }
}