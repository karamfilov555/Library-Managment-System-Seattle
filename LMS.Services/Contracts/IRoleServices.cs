using System;
using System.Collections.Generic;
using System.Text;
using LMS.Data.Models;

namespace LMS.Services.Contracts
{
    public interface IRoleServices
    {
        void AddRoleToDb(Role role);
        bool CheckIfRoleExist(string roleName);
    }
}
