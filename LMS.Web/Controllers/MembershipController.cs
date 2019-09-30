using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LMS.Services.Contracts;
using NToastNotify;

namespace LMS.Web.Controllers
{
    public class MembershipController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMembershipService _membershipService;
        private readonly IToastNotification _toast;

        public MembershipController(IUserService userService, 
                                    IMembershipService membershipService,
                                    IToastNotification toast)
        {
            _userService = userService;
            _membershipService = membershipService;
            _toast = toast;
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
            _toast.AddSuccessToastMessage("You succesfully pay your membership!");
            return RedirectToAction("LogOff", "Auth");
        }
        public IActionResult PaymentSuccess()
        {
            return View();
        }

    }
}