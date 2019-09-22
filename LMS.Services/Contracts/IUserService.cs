using LMS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services.Contracts
{
    public interface IUserService
    {
        Task<ICollection<User>> GetUsersAsync();
        Task<Role> GetUserRole(string userId);
    }
}
