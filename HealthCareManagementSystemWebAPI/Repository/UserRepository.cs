using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareManagementSystemWebAPI.Data;
using HealthCareManagementSystemWebAPI.Interface;
using HealthCareManagementSystemWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthCareManagementSystemWebAPI.Repository
{
    public class UserRepository : IUser
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context  = context;
        }

        public async Task DeleteUser(string id)
        {
            var UserExists = await _context.Users.FindAsync(id);
        
            if(UserExists != null)
            {
                _context.Users.Remove(UserExists);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

       public async Task<List<UserWithRoles>> GetAllUsersWithRolesAsync()
       {
            var users = await _context.Users
                .Select(user => new UserWithRoles
                {
                    Id = user.Id,
                    UserName = user.UserName!,
                    Email = user.Email,
                    Roles = _context.UserRoles
                        .Where(ur => ur.UserId == user.Id)
                        .Select(ur => _context.Roles.FirstOrDefault(r => r.Id == ur.RoleId)!.Name) 
                        .ToList()! 
                })
                .ToListAsync();

            return users;
        }



        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user != null)
            {
                return user;
            }
            throw new KeyNotFoundException($"User with ID {id} not found.");
        }

        public async Task UpdateUser(ApplicationUser user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {user.Id} not found.");
            }
            
            _context.Entry(existingUser).CurrentValues.SetValues(user);
      
            await _context.SaveChangesAsync();
        }
    }
}