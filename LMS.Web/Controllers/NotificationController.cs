using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using LMS.Models;
using LMS.Services.Contracts;
using LMS.Web.Mappers;
using NToastNotify;

namespace LMS.Web.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class NotificationController : Controller
    {
        private readonly UserManager<User> _usermanager;
        private readonly INotificationService _notificationService;
        private readonly IToastNotification _toast;

        public NotificationController(UserManager<User> usermanager,
                                      INotificationService notificationService,
                                      IToastNotification toast)
        {
            _usermanager = usermanager;
            _notificationService = notificationService;
            _toast = toast;
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
            var notification = await _notificationService.MarkAsSeenAsync(Id);
            _toast.AddInfoToastMessage("Message marked as seen.");

            return PartialView("_NotificationSeenPartial", notification.MapToNotificationViewModel());
            //return ok
        }
    }
}