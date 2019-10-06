using LMS.DTOs;
using LMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Services.Contracts
{
    public interface IUserService
    {
        //Task<User> GetCurrentUserAsync();
        Task<ICollection<User>> GetUsersAsync();
        Task<Role> GetUserRoleAsync(string userId);
        Task<User> BanUserAsync(BanDto banDto);
        Task<string> FindUsernameByIdAsync(string userId);
        Task<User> FindUserByUsernameAsync(string username);
         Task<User> FindUserByIdAsync(string userId);
        Task<User> GetAdmin();
        Task SetUserCancelationStatusAsync(string id);
        Task<User> CheckIfUserIsCanceled(string username);
    }
}
