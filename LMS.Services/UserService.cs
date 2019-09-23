using LMS.Data;
using LMS.Models;
using LMS.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class UserService : IUserService
    {
        private readonly LMSContext _context;
        public UserService(LMSContext context)
        {
            _context = context;
        }
        public async Task<ICollection<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<Role> GetUserRole(string userId)
        {
            var roleId = await _context.UserRoles.FindAsync(userId);
            var role = await _context.Roles.FindAsync(roleId);
            return role;
        }
    }
}
