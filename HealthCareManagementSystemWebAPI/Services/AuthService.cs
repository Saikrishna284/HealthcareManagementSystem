using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HealthCareManagementSystemWebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using static HealthCareManagementSystemWebAPI.Models.AuthModel;

namespace HealthCareManagementSystemWebAPI.Services
{
    public class AuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

         private readonly IConfiguration _configuration;

         public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
         {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
         }

        public async Task<AuthResponse?> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return null;
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var token = GenerateJwtToken(user, userRoles);

            return new AuthResponse
            {
                Token = token,
                Username = user.UserName!,
                Email = user.Email!,
                Roles = userRoles.ToList(),
                Expiration = DateTime.Now.AddHours(3)
            };
        }

        public async Task<IdentityResult> Register(RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User already exists!" });
            }

            ApplicationUser user = new ApplicationUser()
            {
                UserName = model.Username,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return result;
            }

            // Check if role exists, create if it doesn't
            if (!await _roleManager.RoleExistsAsync(model.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole(model.Role));
            }

            // Add user to role
            await _userManager.AddToRoleAsync(user, model.Role);

            return result;
        }

        private string GenerateJwtToken(ApplicationUser user, IList<string> roles)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
    }
}