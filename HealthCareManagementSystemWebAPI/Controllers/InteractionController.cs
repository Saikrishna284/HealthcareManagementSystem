using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareManagementSystemWebAPI.Dto;
using HealthCareManagementSystemWebAPI.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthCareManagementSystemWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InteractionController : ControllerBase
    {
        private readonly IInteractionService _service;
        public InteractionController(IInteractionService service)
        {
            _service = service;
        }
        [Authorize(Roles = "ADMIN, DOCTOR")]
        [HttpPost("AddInteraction")]

        public async Task<IActionResult> AddInteraction(InteractionsDto newInteraction)
        {
            try
            {
                await _service.AddInteraction(newInteraction);
                return Ok(new {message = "Interactions updated"});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An internal server error occurred.", error = ex.Message });
            }
        }
        [Authorize(Roles = "ADMIN, DOCTOR, NURSE")]
        [HttpGet("GetInteractionsByPatientsId/{id}")]

        public async Task<IActionResult> GetInteractionsByPatientsId(int id)
        {
            try
            {
                var response = await _service.GetInteractionsByPatientId(id);
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

        [Authorize(Roles = "ADMIN, DOCTOR")]
        [HttpPut("UpdatePatientInteraction/{id}")]
        public async Task<IActionResult> UpdatePatientInteraction([FromRoute]int id, [FromBody]UpdateInteractionsDto interaction)
        {
            try
            {
                if(id != interaction.InteractionId)
                {
                    return BadRequest(new { message = "Interaction Id didn't match"});
                }
                await _service.UpdateIntearction(interaction);
                return Ok(new { message = "Interaction Updated Successfully"});
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An internal server error occurred.", ex.Message} );
            }
        }
    }
}