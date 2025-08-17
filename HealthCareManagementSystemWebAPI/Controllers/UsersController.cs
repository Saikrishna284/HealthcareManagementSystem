using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareManagementSystemWebAPI.Dto;
using HealthCareManagementSystemWebAPI.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthCareManagementSystemWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UsersController : ControllerBase
    {

        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }
        
        [Authorize(Roles = "ADMIN")]
        [HttpGet("GetAllUsers")]

        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var response = await _service.GetUsers();
                if(response.Any())
                {
                    return Ok(response);
                }

                return NotFound(new { message = "No Users were found" });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An internal server error occurred.", error = ex.Message } );
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("GetUserById/{id}")]

        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var response = await _service.GetUserByIdAsync(id);
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
        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdatePatient(string id, UserDto user)
        {
            try
            {
                if (id != user.Id)
                {
                    return BadRequest(new { message = "ID in URL does not match ID in request body" });
                }
                
                await _service.UpdateUser(user);
                return Ok(new {message = "User updated successfully"});
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(409, new { message = "The resource was modified by another process. Please refresh and try again." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorMessage = "An error occurred while updating the user.", details = ex.Message });
            }
        }
        
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute]string id)
        {
            try
            {
                await _service.DeleteUser(id);
                return Ok(new { message = "User deleted Successfully"});
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

         [HttpGet("GetAllUsersWithRoles")]

        public async Task<IActionResult> GetAllUsersWithRoles()
        {
            try
            {
                var response = await _service.GetUsersWithRoles();
                if(response.Any())
                {
                    return Ok(response);
                }

                return NotFound(new { message = "No Users were found" });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An internal server error occurred.", error = ex.Message } );
            }
        }
        
    }
}