using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareManagementSystemWebAPI.Models;

namespace HealthCareManagementSystemWebAPI.Interface
{
    public interface IPatient
    {
        Task AddPatientAsync(Patient patient);
        Task<IEnumerable<Patient>> GetPatientsAsync();
        Task<Patient> GetPatientByIdAsync(int id);

        Task DeletePatient(int id);

        Task UpdatePatient(Patient patient);

    }
}