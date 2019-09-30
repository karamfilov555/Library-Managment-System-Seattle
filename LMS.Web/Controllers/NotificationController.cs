using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Models;
using LMS.Services.Contracts;
using LMS.Web.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NotificationController : Controller
    {
        private readonly UserManager<User> _usermanager;
        private readonly INotificationService _notificationService;

        public NotificationController(UserManager<User> usermanager,
                                      INotificationService notificationService)
        {
            _usermanager = usermanager;
            _notificationService = notificationService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _usermanager.GetUserAsync(User);
            var notifications = await _notificationService.GetNotificationsAsync(user.Id);
            var notificationsVm = notifications.Select(n => n.MapToNotificationViewModel());
            var notificationsVmSortedByDate = notificationsVm.OrderByDescending(n => n.EventDate);
            return View(notificationsVmSortedByDate);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsSeen(string Id)
        {
            await _notificationService.MarkAsSeenAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}