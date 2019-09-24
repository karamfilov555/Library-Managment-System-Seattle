using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LMS.Services.Contracts;
using LMS.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Web.Controllers
{
    public class MembershipController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMembershipService _membershipService;

        public MembershipController(IUserService userService, IMembershipService membershipService)
        {
            _userService = userService;
            _membershipService = membershipService;
        }
        [HttpGet]
     
        public IActionResult Subscribe()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubscribeConfirmation()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = await _userService.FindUsernameByIdAsync(userId);
            await _membershipService.MakeMember(username);
            
            //this.TempData["Success"] = $"User: {username} you successfully pay your membership";

            return RedirectToAction("LogOff", "Auth");
        }
        public IActionResult PaymentSuccess()
        {
            return View();
        }

    }
}