using LMS.Data;
using LMS.Models;
using LMS.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using LMS.Models.Models;
using LMS.DTOs;

namespace LMS.Services
{
    public class UserService : IUserService
    {
        private readonly LMSContext _context;
        private readonly IBanService _banService;

        public UserService(LMSContext context, IBanService banService)
        {
            _context = context;
            _banService = banService;
        }

        public async Task<ICollection<User>> GetUsersAsync()
            => await _context.Users.ToListAsync();

        public async Task<Role> GetUserRole(string userId)
        {
            var roleId = await _context.UserRoles.FindAsync(userId);
            var role = await _context.Roles.FindAsync(roleId);
            return role;
        }

        public async Task<User> FindUserByIdAsync(string userId)
            => await _context.Users.FindAsync(userId);

        public async Task<User> FindUserForBanAsync(string userId)
        {
            var user = await _context.Users
                .Include(u => u.BanRecord)
                .FirstOrDefaultAsync(m => m.Id == userId);
            return user;
        }
        public async Task<string> FindUsernameById(string userId)
        {
            var user = await _context.Users
                .Include(u => u.BanRecord)
                .FirstOrDefaultAsync(m => m.Id == userId);
            return user.UserName;
        }
        public async Task<User> BanUser(BanDto banDto)
        {
            var user = await FindUserForBanAsync(banDto.UserId);
            var ban = await _banService.AddBan(banDto);

            return user;
        }
    }
}
