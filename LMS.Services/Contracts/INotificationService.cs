using LMS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services.Contracts
{
    public interface INotificationService
    {
        Task<Notification> CreateNotificationAsync(string userId, string description);
        Task<ICollection<Notification>> GetNotificationsAsync(string userId);
    }
}
