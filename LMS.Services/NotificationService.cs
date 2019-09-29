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

        public NotificationService(LMSContext context)
        {
            _context = context;
        }
        public async Task<Notification> CreateNotificationAsync(string userId,string description)
        {
            var notification = new Notification
            {
                UserId = userId,
                Description = description,
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
    }
}
