using LMS.Data;
using LMS.Models;
using LMS.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using LMS.Models.Models;
using LMS.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LMS.Services
{
    public class UserService : IUserService
    {
        private readonly LMSContext _context;
        private readonly IBanService _banService;
        private readonly UserManager<User> _userManager;
        private readonly HttpContext _httpContext;

        public UserService(LMSContext context, IBanService banService, UserManager<User> userManager)
        {
            _context = context;
            _banService = banService;
            _userManager = userManager;
        }
        //public async Task<User> GetCurrentUserAsync()
        //{
        //    //var user = await _userManager.GetUserAsync(_httpContext.User);
            
        //    //return user;
        //}
        public async Task<ICollection<User>> GetUsersAsync()
            => await _context.Users.ToListAsync();

        public async Task<Role> GetUserRoleAsync(string userId)
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
        public async Task<string> FindUsernameByIdAsync(string userId)
        {
            var user = await _context.Users
                .Include(u => u.BanRecord)
                .FirstOrDefaultAsync(m => m.Id == userId);
            return user.UserName;
        }
        public async Task<User> BanUserAsync(BanDto banDto)
        {
            var user = await FindUserForBanAsync(banDto.UserId);
            var ban = await _banService.AddBan(banDto);

            return user;
        }
        public async Task<User> FindUserByUsernameAsync(string username)
        {
            var user = await _context.Users
                .Include(u => u.BanRecord)
                .FirstOrDefaultAsync(m => m.UserName == username);
            return user;
        }
        public async Task<User> GetAdmin()
        {
            var adminRole = await _context.Roles.FirstAsync(role => role.Name.ToLower() == "admin");
            var adminId = await _context.UserRoles.FirstAsync(roleId => roleId.RoleId == adminRole.Id);
            var admin = await FindUserByIdAsync(adminId.UserId);
            return admin;
        }
    }
}
