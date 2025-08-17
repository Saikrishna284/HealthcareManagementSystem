using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareManagementSystemWebAPI.Data;
using HealthCareManagementSystemWebAPI.Interface;
using HealthCareManagementSystemWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace HealthCareManagementSystemWebAPI.Repository
{
    public class PatientRepository : IPatient
    {
        private ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddPatientAsync(Patient patient)
        {
           await _context.Patients.AddAsync(patient);
           await _context.SaveChangesAsync();
           
        }

        public async Task DeletePatient(int id)
        {
            var PatientExists = await _context.Patients.FindAsync(id);
            if(PatientExists != null)
            {
                _context.Patients.Remove(PatientExists);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Patient with ID {id} not found.");
            }

        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            var patient = await _context.Patients.Include(i => i.Interactions).FirstOrDefaultAsync(i => i.PatientId == id);
            if(patient != null)
            {
                return patient;
            }
            throw new KeyNotFoundException($"Patient with ID {id} not found.");
        }

        public async Task<IEnumerable<Patient>> GetPatientsAsync()
        {
            var AllPatients = await _context.Patients.Include(i => i.Interactions).ToListAsync();
            return AllPatients;
        }

        public async Task UpdatePatient(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();

        }
    }
}