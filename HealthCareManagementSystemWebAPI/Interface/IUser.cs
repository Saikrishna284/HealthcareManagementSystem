using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareManagementSystemWebAPI.Models;

namespace HealthCareManagementSystemWebAPI.Interface
{
    public interface IUser
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();

        Task<List<UserWithRoles>> GetAllUsersWithRolesAsync();

        Task<ApplicationUser> GetUserByIdAsync(string id);

        Task DeleteUser(string id);

        Task UpdateUser(ApplicationUser user);

    }
}