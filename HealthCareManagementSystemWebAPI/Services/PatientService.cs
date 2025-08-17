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
    public class PatientService : IPatientService
    {
        private readonly IPatient _repo;

        private readonly IMapper _mapper;
        public PatientService(IPatient repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task AddPatientAsync(AddPatientDto patient)
        {
            try
            {
                var patientMap = _mapper.Map<Patient>(patient);
                await _repo.AddPatientAsync(patientMap);
            }
            catch(Exception ex)
            {
                throw new Exception("Error occurred while adding patient in service layer.", ex);
            }
        }

        public async Task DeletePatient(int id)
        {
           try
           {
              await _repo.DeletePatient(id);
           }
           catch(KeyNotFoundException)
           {
              throw;
           }
           catch(Exception ex)
           {
              throw new Exception("Error occurred while deleting patient in service layer.", ex);
           }
           
        }

        public async Task<GetPatientDto> GetPatientByIdAsync(int id)
        {
            try
            {
                var patient = await _repo.GetPatientByIdAsync(id);
                return _mapper.Map<GetPatientDto>(patient);
            }
            catch(KeyNotFoundException)
            {
                throw;
            }
            catch(Exception ex)
            {
               throw new Exception("Error occurred while fetching patient in service layer.", ex);
            }
        }

        public async Task<IEnumerable<GetPatientDto>> GetPatientsAsync()
        {
            try
            {
               var patients  = await _repo.GetPatientsAsync();
               return _mapper.Map<IEnumerable<GetPatientDto>>(patients);
            }
            catch(Exception ex)
            {
               throw new Exception("Error occurred while fetching patients in service layer.", ex);
            }
        }

        public async Task UpdatePatient(UpdatePatientDto patient)
        {
            try
            {
                var patientMap = _mapper.Map<Patient>(patient);
                await _repo.UpdatePatient(patientMap);
            }
            catch(Exception ex)
            {
               throw new Exception("Error occurred while Updating patients in service layer.", ex);
            }

        }
    }
}