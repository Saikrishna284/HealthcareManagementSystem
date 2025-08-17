using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareManagementSystemWebAPI.Dto;
using HealthCareManagementSystemWebAPI.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HealthCareManagementSystemWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _service;
        public PatientController(IPatientService service)
        {
            _service = service;
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("AddPatient")]
        public async Task<IActionResult> AddPatient([FromBody] AddPatientDto patient)
        {
            try
            {
              await _service.AddPatientAsync(patient);
              return Ok(new { message = "Patient added successfully"});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An internal server error occurred.", error = ex.Message });
            }
        }

        [Authorize(Roles = "ADMIN, DOCTOR, NURSE")]

        [HttpGet("GetAllPatients")]

        public async Task<IActionResult> GetAllPatients()
        {
            try
            {
                var response = await _service.GetPatientsAsync();
                if(response.Any())
                {
                    return Ok(response);
                }
                return NotFound(new {message = "No patients were found"});
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An internal server error occurred.", error = ex.Message } );
            }
        }

        [Authorize(Roles = "ADMIN, DOCTOR, NURSE")]
        [HttpGet("GetPatientById/{id}")]
    
        public async Task<IActionResult> GetPatientById([FromRoute]int id)
        {
            try
            {
                var response = await _service.GetPatientByIdAsync(id);
                return Ok(response);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch(Exception)
            {
                return StatusCode(500, new { message = "An internal server error occurred." } );
            }
        }
         [Authorize(Roles = "ADMIN")]
        [HttpDelete("DeletePatient/{id}")]
        public async Task<IActionResult> DeletePatient([FromRoute]int id)
        {
            try
            {
                await _service.DeletePatient(id);
                return Ok(new { message = "Patient deleted Successfully"});
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch(Exception)
            {
                return StatusCode(500, new { message = "An internal server error occurred." } );
            }
        }
        [Authorize(Roles = "ADMIN, DOCTOR")]
        [HttpPut("UpdatePatient/{id}")]
        public async Task<IActionResult> UpdatePatient([FromRoute]int id, [FromBody] UpdatePatientDto patient)
        {
            try
            {
                if(id != patient.PatientId)
                {
                    return BadRequest(new { message = "Patient Id didn't match"});
                }
                await _service.UpdatePatient(patient);
                return Ok(new { message = "Patient Updated Successfully"});
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An internal server error occurred.", ex.Message} );
            }
        }
    }
}