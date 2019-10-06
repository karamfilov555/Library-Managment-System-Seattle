﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using LMS.Models;
using LMS.Services.Contracts;
using LMS.Web.Models;
using LMS.Web.Mappers;
using LMS.Web.Mappers.Contracts;
using System.Collections.Generic;

namespace LMS.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IRoleManagerService _roleManager;
        private readonly IUserService _userService;
        private readonly IMapVmToDTO _dtoMapper;

        public AdminController(UserManager<User> userManager,
                               IRoleManagerService roleManager,
                               IUserService userService,
                               IMapVmToDTO dtoMapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userService = userService;
            _dtoMapper = dtoMapper;
        }
        [Route(nameof(ListUsers))]
        public async Task<IActionResult> ListUsers()
        {
            var users = await _userService.GetUsersAsync();
            var listUsersVm = new ListUsersViewModel();
            var usersVm = new List<UserViewModel>();
            string roleName;
            foreach (var user in users)
            {
                var role = await _roleManager.GetUserRole(user.Id);
                if (role == null)
                {
                    roleName = "FreeAccount";
                }
                else
                {
                    roleName = role.Name;
                }
                    var userVm = user.MapToUserViewModel(roleName);
                    usersVm.Add(userVm);
            }
            listUsersVm.Users = usersVm;
            return View(listUsersVm);
        }
        [HttpPost]
        [Route(nameof(CreateLibrarian) + "/{userId}")]
        public async Task<IActionResult> CreateLibrarian(string userId)
        {
            var role = await _roleManager.GetUserRole(userId);
            var roleName = role.Name;
            if (roleName.ToLower() == "member" || roleName.ToLower() == "admin")
            {
                ViewBag.ErrorTitle = $"You are tring to set user in role {roleName} to librarian";
                ViewBag.ErrorMessage = $"Users in role \"{roleName}\" cannot be Librarians!";
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.AddToRoleAsync(user, "Librarian");

            this.TempData["Success"] = $"User: {user.UserName} successfully added to librarian role!";

            return RedirectToAction("ListUsers", "Admin");
        }
        [HttpPost]
        [Route(nameof(CreateMember) + "/{userId}")]
        public async Task<IActionResult> CreateMember(string userId)
        {
            var role = await _roleManager.GetUserRole(userId);
            var roleName = role.Name;
            if (roleName.ToLower() == "admin" || roleName.ToLower() == "librarian")
            {
                ViewBag.ErrorTitle = $"You are tring to demote user in role {roleName}";
                ViewBag.ErrorMessage = $"Users in role \"{roleName}\" cannot be members!";
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.AddToRoleAsync(user, "Member");

            this.TempData["Success"] = $"User: {user.UserName} successfully added to member role!";

            return RedirectToAction("ListUsers", "Admin");
        }

        [HttpGet]
        [Route(nameof(BanUser) + "/{userId}")]
        public async Task<IActionResult> BanUser(string userId)
        {
            var role = await _roleManager.GetUserRole(userId);
            var roleName = role.Name;

            if (roleName.ToLower() == "admin")
            {
                ViewBag.ErrorTitle = $"You are tring to ban user in role administrator";
                ViewBag.ErrorMessage = "Users in role \"Administrator\" cannot be banned!";
                return View("Error");
            }
            else
            {
                var username = await _userService.FindUsernameByIdAsync(userId);
                var vm = new BanViewModel
                {
                    UserId = userId,
                    Username = username,
                };
                return View(vm);
            }
        }

        [Authorize(Roles = "Librarian, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BanUserConfirmation(BanViewModel vm)
        {
            var role = await _roleManager.GetUserRole(vm.UserId);
            var roleName = role.Name;
            if (roleName.ToLower() == "admin")
            {
                ViewBag.ErrorTitle = $"You are tring to ban user in role administrator";
                ViewBag.ErrorMessage = "Users in role \"Administrator\" cannot be banned!";
                return View("Error");
            }
            var banDto = await _dtoMapper.MapBanVmToDto(vm);
            //var user = await _userManager.FindByIdAsync(userId);
            var user = await _userService.BanUserAsync(banDto);

            this.TempData["Success"] = $"User: {user.UserName} successfully Banned";

            return RedirectToAction("ListUsers", "Admin");
        }
    }
}