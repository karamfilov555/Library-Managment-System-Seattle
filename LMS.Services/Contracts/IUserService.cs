using LMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Services.Contracts
{
    public interface IUserService
    {
        Task<ICollection<User>> GetUsersAsync();
        Task<Role> GetUserRole(string userId);
    }
}
