using System;
using System.Collections.Generic;
using System.Text;
using LMS.Models;

namespace LMS.Services.Contracts
{
    public interface IRoleServices
    {
        void AddRoleToDb(Role role);
        bool CheckIfRoleExist(string roleName);
        Role FindRoleInDb(string name);
        Role ProvideRole(string roleName);
    }
}
