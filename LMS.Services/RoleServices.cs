using System;
using System.Collections.Generic;
using System.Text;
using LMS.Data;
using LMS.Models;
using LMS.Services.Contracts;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using LMS.Services.ModelProviders.Contracts;

namespace LMS.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly LMSContext _context;
        private readonly IRoleFactory _roleFactory;

        public RoleServices(LMSContext context, 
                            IRoleFactory roleFactory)
        {
            _context = context;
            _roleFactory = roleFactory;
        }
        public bool CheckIfRoleExist(string roleName)
        {
            if (_context.Roles.Any(r => r.Name == roleName))
                return true;
            return false;
        }
        public void AddRoleToDb(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
        }
        public Role FindRoleInDb(string name)
        {
            var role = _context.Roles.FirstOrDefault(c => c.Name == name);
            return role;
        }
        public Role ProvideRole(string roleName)
        {
            if (!CheckIfRoleExist(roleName))
            {
                var role = _roleFactory.CreateRole(roleName);
                AddRoleToDb(role);
                return role;
            }
            else
                return FindRoleInDb(roleName);
        }
    }
}
