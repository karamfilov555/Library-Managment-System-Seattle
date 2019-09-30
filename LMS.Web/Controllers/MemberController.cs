using System.Security.Claims;
using System.Threading.Tasks;
using LMS.Models;
using LMS.Services.Contracts;
using LMS.Web.Mappers;
using LMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace LMS.Web.Controllers
{
    [Authorize(Roles = "Member")]
    public class MemberController : Controller
    {
        private readonly IHistoryService _historyService;
        private readonly IBookService _bookService;
        private readonly INotificationService _notificationService;
        private readonly INotificationManager _notificationManager;
        private readonly UserManager<User> _userManager;
        private readonly IToastNotification _toast;

        public MemberController(IHistoryService historyService,
                                IBookService bookService,
                                INotificationService notificationService,
                                INotificationManager notificationManager,
                                UserManager<User> userManager,
                                IToastNotification toast)
        {
            _historyService = historyService;
            _bookService = bookService;
            _notificationService = notificationService;
            _notificationManager = notificationManager;
            _userManager = userManager;
            _toast = toast;
        }
        public IActionResult Index()
        {
            return View();
        }

        //[Route(nameof(CheckoutBook) + "/{userId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> CheckoutBook(string Id)//vm
        {
            if (Id == null)
                return NotFound();
            ClaimsPrincipal currUser = this.User;
            var userId = currUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            //todo is book free
            var book = await _bookService.FindFreeBookByIdAsync(Id);
            if (book == null)
                return NotFound();
            if (book.Copies <= 0)
            {
                //To do .. da preprashta v reservaciqta za tazi kniga
                return NotFound();
            }
            //listbookVm copies --;
            var hr = await _historyService.CheckoutBookAsync(Id,userId);

            var checkoutBookVm = new CheckoutBookViewModel
            {
                Title = book.Title,
                AuthorName = book.Author.Name,
                Country = book.Country,
                ReturnDate = hr.ReturnDate,
                SubjectCategoryName = book.SubjectCategory.Name,
                Pages = book.Pages,
                Year = book.Year,
                Language = book.Language,
                CoverImageUrl = book.CoverImageUrl
            };
            //TODO: try-catch!!!!! ( catch exeption (return BadRequest(ex msg)) !!!
            var user = await _userManager.GetUserAsync(User);
            var username = user.UserName;

            var notificationDescription = _notificationManager.CheckOutBookDescription(username, book.Title);
            var notification = await _notificationService.CreateNotificationAsync(notificationDescription, username);
            _toast.AddSuccessToastMessage("You checked out book successfully!");
            return View(checkoutBookVm);
        }

        [Authorize(Roles = "Member")]
        public async Task<IActionResult> MyBooks()
        {
            var user = await _userManager.GetUserAsync(User);

            var checkouts = await _historyService.GetCheckOutsOfUserAsync(user.Id);

            var booksVm = checkouts.MapToCheckOutViewModel();

            var msg = (string)TempData["ReturnMsg"];
            ViewBag.Date = msg;
            return View(booksVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> ReturnBookAsync(string Id)
        {
            if (Id == null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);
            await _historyService.ReturnBookAsync(Id , user.Id);

            var title = await _bookService.GetBookTitleAsync(Id);
            var username = user.UserName;

            var notificationDescription = _notificationManager.ReturnBookDescription(username, title);
            var notification = await _notificationService.CreateNotificationAsync(notificationDescription, username);

            TempData["ReturnMsg"] = ($"{username}, you successfully returned a book: \"{title}\"!");
            return RedirectToAction(nameof(MyBooks));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> RenewBookAsync(string Id)
        {
            if (Id == null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);
            var hr = await _historyService.RenewBookAsync(Id, user.Id);

            var title = await _bookService.GetBookTitleAsync(Id);
            var username = user.UserName;

            var notificationDescription = _notificationManager.RenewBookDescription(username, hr.ReturnDate, title);

            var notification = await _notificationService.CreateNotificationAsync(notificationDescription, username);
            // tuk trqbva da podavam Id-to na User-a polu4atel, a ne na segashniq
            TempData["ReturnMsg"] = ($"{username}, you successfully renew return date of a book: \"{title}\" to {hr.ReturnDate} !");

            return RedirectToAction(nameof(MyBooks));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> ReserveBook(string Id)
        {
            if (Id == null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);
            var hr = await _historyService.RenewBookAsync(Id, user.Id);

            var title = await _bookService.GetBookTitleAsync(Id);
            var username = user.UserName;

            var notificationDescription = _notificationManager.RenewBookDescription(username, hr.ReturnDate, title);

            var notification = await _notificationService.CreateNotificationAsync(notificationDescription, username);
            // tuk trqbva da podavam Id-to na User-a polu4atel, a ne na segashniq
            TempData["ReturnMsg"] = ($"{username}, you successfully renew return date of a book: \"{title}\" to {hr.ReturnDate} !");

            return RedirectToAction(nameof(MyBooks));
        }
    }
}