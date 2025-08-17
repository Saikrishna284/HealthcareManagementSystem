using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HealthCareManagementSystemWebAPI.Dto;
using HealthCareManagementSystemWebAPI.Interface;
using HealthCareManagementSystemWebAPI.Models;

namespace HealthCareManagementSystemWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUser _repo;
        private readonly IMapper _mapper;

        public UserService(IUser repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task DeleteUser(string id)
        {
           try
           {
              await _repo.DeleteUser(id);
           }
           catch(KeyNotFoundException)
           {
              throw;
           }
           catch(Exception ex)
           {
              throw new Exception("Error occurred while deleting user in service layer.", ex);
           }
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            try
            {
                var user = await _repo.GetUserByIdAsync(id);
                return _mapper.Map<UserDto>(user);
            }
            catch(KeyNotFoundException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new Exception("Error occurred while fetching User in service layer.", ex);
            }
            
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            try
            {
                var users = await _repo.GetAllUsersAsync();
                return _mapper.Map<IEnumerable<UserDto>>(users);
            }
            catch(Exception ex)
            {
                throw new Exception("Error occurred while fetching Users in service layer.", ex);
            }
           
        }

        public async Task<List<UserWithRolesDto>> GetUsersWithRoles()
        {
           try
            {
                var users = await _repo.GetAllUsersWithRolesAsync();
                return _mapper.Map<List<UserWithRolesDto>>(users);
            }
            catch(Exception ex)
            {
                throw new Exception("Error occurred while fetching Users in service layer.", ex);
            }
        }

        public async Task UpdateUser(UserDto user)
        {
            try
            {
               var userMap = _mapper.Map<ApplicationUser>(user);
               await _repo.UpdateUser(userMap);
            }
            catch(KeyNotFoundException)
            {
                throw;
            }
            catch(Exception)
            {
                throw new Exception("Error occurred while Updating patients in service layer.");
            }
        }
    }
}