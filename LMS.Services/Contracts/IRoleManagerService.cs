using LMS.Models;
using System.Threading.Tasks;

namespace LMS.Services.Contracts
{
    public interface IRoleManagerService
    {
        Task<Role> GetUserRole(string userId);
        Task<string> GetUserRoleName(string userId);
    }
}