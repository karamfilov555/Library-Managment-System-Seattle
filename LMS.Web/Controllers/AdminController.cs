using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using LMS.Models;
using LMS.Services.Contracts;
using LMS.Web.Models;
using LMS.Web.Mappers;
using LMS.Web.Mappers.Contracts;

namespace LMS.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IMapVmToDTO _dtoMapper;

        public AdminController(UserManager<User> userManager, 
                               IUserService userService,
                               IMapVmToDTO dtoMapper)
        {
            _userManager = userManager;
            _userService = userService;
            _dtoMapper = dtoMapper;
        }
        [Route(nameof(ListUsers))]
        public async Task<IActionResult> ListUsers()
        {
            var users = await _userService.GetUsersAsync();
            var listUsersVm = new ListUsersViewModel();
            listUsersVm.Users = users.Select(user => user.MapToUserViewModel()).ToList();
            return View(listUsersVm);
        }
        [HttpPost]
        [Route(nameof(CreateLibrarian) + "/{userId}")]
        public async Task<IActionResult> CreateLibrarian(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.AddToRoleAsync(user, "Librarian");

            this.TempData["Success"] = $"User: {user.UserName} successfully added to librarian role!";

            return RedirectToAction("ListUsers", "Admin");
        }
        [HttpPost]
        [Route(nameof(CreateMember) + "/{userId}")]
        public async Task<IActionResult> CreateMember(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.AddToRoleAsync(user, "Member");

            this.TempData["Success"] = $"User: {user.UserName} successfully added to member role!";

            return RedirectToAction("ListUsers", "Admin");
        }

        [HttpGet]
        [Route(nameof(BanUser) + "/{userId}")]
        public async Task<IActionResult> BanUser(string userId)
        {
            var username = await _userService.FindUsernameById(userId);
            var vm = new BanViewModel
            {
                UserId = userId ,
                Username = username,
            };
            return View(vm);
        }

        [Authorize(Roles = "Librarian, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BanUserConfirmation(BanViewModel vm)
        {
            var banDto = await _dtoMapper.MapBanVmToDto(vm);
            //var user = await _userManager.FindByIdAsync(userId);
            var user = await _userService.BanUser(banDto);

            this.TempData["Success"] = $"User: {user.UserName} successfully Banned";

            return RedirectToAction("ListUsers", "Admin");
        }
    }
}