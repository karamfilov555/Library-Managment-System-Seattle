using System;
using System.Collections.Generic;
using System.Text;
using LMS.Data;
using LMS.Data.Models;
using LMS.Services.Contracts;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;

namespace LMS.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly LMSContext _context;

        public RoleServices(LMSContext context)
        {
            _context = context;
        }
        public bool CheckIfRoleExist(string roleName)
        {
            _context.Role.Any(r=>r.Name == roleName);
            return  true;
        }
        public void AddRoleToDb(Role role)
        {
            _context.Role.Add(role);
            _context.SaveChanges();
        }
    }
}
