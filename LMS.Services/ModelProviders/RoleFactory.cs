using LMS.Models;
using LMS.Services.Contracts;
using LMS.Services.ModelProviders.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services.ModelProviders
{
    public class RoleFactory : IRoleFactory
    {
        public RoleFactory()
        {
        }
        public Role CreateRole(string name)
        {
            var role = new Role(name);
            return role;
        }
    }
}
