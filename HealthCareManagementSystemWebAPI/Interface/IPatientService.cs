using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareManagementSystemWebAPI.Dto;

namespace HealthCareManagementSystemWebAPI.Interface
{
    public interface IPatientService
    {
        Task AddPatientAsync(AddPatientDto patient);
        Task<IEnumerable<GetPatientDto>> GetPatientsAsync();
        Task<GetPatientDto> GetPatientByIdAsync(int id);

        Task DeletePatient(int id);

        Task UpdatePatient(UpdatePatientDto patient);
    }
}