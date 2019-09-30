using LMS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services.Contracts
{
    public interface INotificationService
    {
        Task<Notification> CreateNotificationAsync(string description, string username);
        Task<ICollection<Notification>> GetNotificationsAsync(string userId);
        Task<int> GetNotificationsCountAsync();
        Task MarkAsSeenAsync(string notificationId);
    }
}
