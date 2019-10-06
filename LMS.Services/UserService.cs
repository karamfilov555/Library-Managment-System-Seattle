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

        public UserService(LMSContext context,
                           IBanService banService
                           )
        {
            _context = context;
            _banService = banService;

        }
        //public async Task<User> GetCurrentUserAsync()
        //{
        //    //var user = await _userManager.GetUserAsync(_httpContext.User);

        //    //return user;
        //}
        public async Task<ICollection<User>> GetUsersAsync()
            => await _context.Users.ToListAsync().ConfigureAwait(false);

        public async Task<Role> GetUserRoleAsync(string userId)
        {
            var roleId = await _context.UserRoles.FindAsync(userId).ConfigureAwait(false);
            var role = await _context.Roles.FindAsync(roleId).ConfigureAwait(false);
            return role;
        }

        public async Task<User> FindUserByIdAsync(string userId)
            => await _context.Users.FindAsync(userId).ConfigureAwait(false);

        public async Task<User> FindUserForBanAsync(string userId)
        {
            var user = await _context.Users
                .Include(u => u.BanRecord)
                .FirstOrDefaultAsync(m => m.Id == userId).ConfigureAwait(false);
            return user;
        }
        public async Task<string> FindUsernameByIdAsync(string userId)
        {
            var user = await _context.Users
                .Include(u => u.BanRecord)
                .FirstOrDefaultAsync(m => m.Id == userId).ConfigureAwait(false);
            return user.UserName;
        }
        public async Task<User> BanUserAsync(BanDto banDto)
        {
            var user = await FindUserForBanAsync(banDto?.UserId).ConfigureAwait(false);
            var ban = await _banService.AddBan(banDto).ConfigureAwait(false);

            return user;
        }
        public async Task<User> FindUserByUsernameAsync(string username)
        {
            var user = await _context.Users
                .Include(u => u.BanRecord)
                .FirstOrDefaultAsync(m => m.UserName == username).ConfigureAwait(false);
            return user;
        }
        public async Task<User> GetAdmin()
        {
            var adminRole = await _context.Roles.FirstAsync(role => role.Name.ToLower() == "admin").ConfigureAwait(false);
            var adminId = await _context.UserRoles.FirstAsync(roleId => roleId.RoleId == adminRole.Id).ConfigureAwait(false);
            var admin = await FindUserByIdAsync(adminId.UserId).ConfigureAwait(false);
            return admin;
        }
        public async Task SetUserCancelationStatusAsync(string id)
        {
            var user = await FindUserByIdAsync(id).ConfigureAwait(false);
            //await DeleteAllNotificationsOfUser(id).ConfigureAwait(false);
            await DeleteAllReservationsOfUser(id).ConfigureAwait(false);
            user.IsCancelled = true;
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
        private async Task DeleteAllNotificationsOfUser(string userId)
        {
            var notifications = _context.Notifications.Where(n => n.UserId == userId);
            _context.Notifications.RemoveRange(notifications);
        }
        private async Task DeleteAllReservationsOfUser(string userId)
        {
            var reservations = _context.ReservedBooks.Where(n => n.UserId == userId);
            _context.ReservedBooks.RemoveRange(reservations);
        }
        public async Task<User> CheckIfUserIsCanceled(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username).ConfigureAwait(false);
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}
