using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LMS.Services.Contracts;
using NToastNotify;
using Microsoft.AspNetCore.Identity;
using LMS.Models;
using LMS.Web.Mappers;
using Microsoft.AspNetCore.Authorization;

namespace LMS.Web.Controllers
{
    public class MembershipController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMembershipService _membershipService;
        private readonly IHistoryService _historyService;
        private readonly IToastNotification _toast;
        private readonly INotificationManager _notificationManager;
        private readonly INotificationService _notificationService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public MembershipController(IUserService userService,
                                    IMembershipService membershipService,
                                    IHistoryService historyService,
                                    INotificationManager notificationManager,
                                    INotificationService notificationService,
                                    IToastNotification toast,
                                    SignInManager<User> signInManager,
                                    UserManager<User> userManager)
        {
            _userService = userService;
            _membershipService = membershipService;
            _notificationManager = notificationManager;
            _historyService = historyService;
            _notificationService = notificationService;
            _toast = toast;
            _userManager = userManager;
            _signInManager = signInManager;
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
        public async Task<IActionResult> PaymentSuccess()
        {
            return View();
        }
        public async Task<IActionResult> CancelMembership()
        {
            var user = await _userManager.GetUserAsync(User);
            var userVm = user.MapToUserViewModel();
            return View(userVm);
        }
        
        public async Task<IActionResult> CancelMembershipConfirmation()
        {
            var user = await _userManager.GetUserAsync(User);
            // return all books of a user && cancel all reservations 
            await _historyService.AutoReturnAllBooksOfUser(user.Id);

            //notification to admin
            var description = _notificationManager.CancelMembershipDescription(user.UserName,user.Id);
            var notification = await _notificationService.CreateNotificationAsync(description, user.UserName);

            //signOut and remove from Db
            await _signInManager.SignOutAsync();
            await _userService.DeleteUserAsync(user.Id);


            _toast.AddInfoToastMessage("Your membership was canceled successfully");
            return RedirectToAction("Index","Home");
        }
    }
}