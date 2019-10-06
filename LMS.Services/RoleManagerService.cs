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
            Role role = null;
            var roleId = await _context.UserRoles.FirstOrDefaultAsync(r => r.UserId == userId);
            if (roleId != null)
            {
                role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId.RoleId);
               
            }
            return role;
        }
        public async Task<string> GetUserRoleName(string userId)
        {
            Role role = null;
            string roleName = "FreeAccount";
            var roleId = await _context.UserRoles.FirstOrDefaultAsync(r => r.UserId == userId);
            if (roleId != null)
            {
                role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId.RoleId);
                if (role != null)
                {
                    roleName = role.Name;
                }
                else
                {
                    return roleName;
                }
            }
            return roleName;
        }
    }
}
