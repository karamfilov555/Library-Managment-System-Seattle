using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace LMS.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly INotificationManager _notificationManager;
        private readonly IToastNotification _toast;

        public ContactController(INotificationService notificationService,
                                INotificationManager notificationManager,
                                 IToastNotification toast)
        {
            _notificationService = notificationService;
            _notificationManager = notificationManager;
            _toast = toast;
        }
        public async Task<IActionResult> SendQuickMessage(string name,string email,string message)
        {
            var description = _notificationManager.QuickMessageDescription(message, email);
            await _notificationService.CreateNotificationAsync(description, name);
            _toast.AddSuccessToastMessage("You successfully contact our support!");
            return RedirectToAction("Index","Home");
        }
    }
}