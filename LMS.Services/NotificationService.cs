using LMS.Data;
using LMS.Models;
using LMS.Models.Models;
using LMS.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class NotificationService : INotificationService
    {
        private readonly LMSContext _context;
        private readonly IUserService _userService;

        public NotificationService(LMSContext context,
                                   IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public async Task<Notification> CreateNotificationAsync(string description,string username)
        {
            var admin = await _userService.GetAdmin();

            var notification = new Notification
            {
                UserId = admin.Id,
                Description = description,
                EventDate = DateTime.Now,
                Username = username,
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            return notification;
        }
        public async Task<Notification> SendNotificationToUserAsync(string description, string username)
        {
            var user = await _userService.FindUserByUsernameAsync(username);
            var notification = new Notification
            {
                UserId = user.Id,
                Description = description,
                EventDate = DateTime.Now,
                Username = "System",
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            return notification;
        }
        public async Task<ICollection<Notification>> GetNotificationsAsync(string userId)
        {
            var notification = await _context.Notifications
                                             .Where(n => n.UserId == userId)
                                             .ToListAsync();
            return notification;
        }

        public async Task<int> GetNotificationsCountAsync(string userId)
        {
            var notificationsCount = await _context.Notifications
                                                   .Where(n=>n.IsSeen == false && n.UserId == userId)
                                                   .CountAsync();
            return notificationsCount;
            //latter it will be for unread notifications (have to add boolean isRead in DB)
        }
        public async Task<Notification> MarkAsSeenAsync(string notificationId)
        {
            var notificationToSee = await _context.Notifications
                                                   .FirstAsync(n => n.Id == notificationId);
            notificationToSee.IsSeen = true;
            await _context.SaveChangesAsync();

            return notificationToSee;
            //latter it will be for unread notifications (have to add boolean isRead in DB)
        }
        
    }
}
