using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareManagementSystemWebAPI.Data;
using HealthCareManagementSystemWebAPI.Interface;
using HealthCareManagementSystemWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthCareManagementSystemWebAPI.Repository
{
    public class InteractionRepository : IInteractions
    {
        private readonly ApplicationDbContext _context;
        public InteractionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddInteraction(Interaction interaction)
        {
            await _context.Interactions.AddAsync(interaction);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateInteractions(Interaction  interaction)
        {
            _context.Interactions.Update(interaction);
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Interaction>> GetInteractionsByPatientId(int id)
        {
            var patientExists = await _context.Interactions.Where(p => p.PatientId == id).ToListAsync();
         
            if(patientExists != null)
            {
                return patientExists;
            }
            else
            {
                throw new KeyNotFoundException($"Patient with ID {id} not found.");
            }
        
        }
    }
}