using LMS.Data;
using LMS.Models;
using LMS.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class RoleManagerService : IRoleManagerService
    {
        private readonly LMSContext _context;

        public RoleManagerService(LMSContext context)
        {
            _context = context;
        }
        public async Task<Role> GetUserRole(string userId)
        {
            var roleId = await _context.UserRoles.FirstOrDefaultAsync(r => r.UserId == userId);
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId.RoleId);
            return role;
        }
    }
}
