using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using LMS.Models;
using LMS.Services.Contracts;
using LMS.Web.Models;
using LMS.Web.Mappers;

namespace LMS.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IUserService _userService;

        public AdminController(UserManager<User> userManager, 
                               IUserService userService)
        {
            this.userManager = userManager;
            _userService = userService;
        }
        [Route(nameof(ListUsers))]
        public async Task<IActionResult> ListUsers()
        {
            var users = await _userService.GetUsersAsync();
            var listUsersVm = new ListUsersViewModel();
            listUsersVm.Users = users.Select(user => user.MapToViewModel()).ToList();
            return View(listUsersVm);
        }
        [HttpPost]
        [Route(nameof(CreateLibrarian) + "/{userId}")]
        public async Task<IActionResult> CreateLibrarian(string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);
            await this.userManager.AddToRoleAsync(user, "Librarian");

            this.TempData["Success"] = $"User: {user.UserName} successfully added to librarian role!";

            return RedirectToAction("ListUsers", "Admin");
        }
        [HttpPost]
        [Route(nameof(CreateMember) + "/{userId}")]
        public async Task<IActionResult> CreateMember(string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);
            await this.userManager.AddToRoleAsync(user, "Member");

            this.TempData["Success"] = $"User: {user.UserName} successfully added to member role!";

            return RedirectToAction("ListUsers", "Admin");
        }
    }
}