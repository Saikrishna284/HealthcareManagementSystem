using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareManagementSystemWebAPI.Dto;
using HealthCareManagementSystemWebAPI.Models;

namespace HealthCareManagementSystemWebAPI.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsers();

        Task<List<UserWithRolesDto>> GetUsersWithRoles();

        Task<UserDto> GetUserByIdAsync(string id);

        Task DeleteUser(string id);

        Task UpdateUser(UserDto user);
    }
}