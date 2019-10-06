using System.Linq;
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
        private readonly IReviewService _reviewService;
        private readonly IReservationService _reservationService;
        private readonly IUserService _userService;

        public MemberController(IHistoryService historyService,
                                IBookService bookService,
                                INotificationService notificationService,
                                INotificationManager notificationManager,
                                UserManager<User> userManager,
                                IToastNotification toast,
                                IReviewService reviewService,
                                IReservationService reservationService,
                                IUserService userService)
        {
            _historyService = historyService;
            _bookService = bookService;
            _notificationService = notificationService;
            _notificationManager = notificationManager;
            _userManager = userManager;
            _toast = toast;
            _reviewService = reviewService;
            _reservationService = reservationService;
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet] 
        public async Task<IActionResult> ReviewBook(string Id)
        {
            if (Id == null)
            {
                ViewBag.ErrorTitle = $"You are tring to Review a book with invalid state!";
                ViewBag.ErrorMessage = "Book Id cannot be null!";
                return View("Error");
            }
            var book = await _bookService.FindByIdAsync(Id);
            if (book == null)
            {
                ViewBag.ErrorTitle = $"You are tring to CheckOut a book with invalid model state!";
                return View("Error");
            }
            var books = await _bookService.GetAllSameBooks(Id);
            var user = await _userManager.GetUserAsync(User);
            if (user.Id == null)
            {
                ViewBag.ErrorTitle = $"You are tring to Review a book with invalid User state!";
                ViewBag.ErrorMessage = "User Id cannot be null!";
                return View("Error");
            }

            var canUserReview = await _reviewService.CheckIfUserCanReview(user.Id, Id);
         
            var vm = MapToViewModel.MapToReviewViewModel(books.First(), user.Id);
            vm.Id = Id;
            vm.UserId = user.Id;
                
            vm.CanReview = canUserReview;
            vm.Comments = await _reviewService.GetAllCommentsForBook(vm.Title);
            return View("ReviewBook",vm);
        }
        [HttpPost]
        public async Task<IActionResult> ReviewBook(ReviewViewModel vm)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user.Id == null || vm.Id == null)
            {
                ViewBag.ErrorTitle = $"You are tring to Review a book with invalid User state!";
                ViewBag.ErrorMessage = "User or review Id cannot be null!";
                return View("Error");
            }
            var sameBooks = await _bookService.GetAllSameBooks(vm.Id);
            if (await _reviewService.CheckIfUserCanReview(user.Id, vm.Id))
            {
                //var sameBooksVm = sameBooks.Select(b => b.MapToReviewViewModel(user.Id)).ToList();
                await _reviewService.CreateReviewAsync(user.Id, (decimal)vm.Grade, vm.Description,vm.Id);
                return RedirectToAction("Index", "Book");
            }

            return BadRequest("You cannot review book you have already reviewed!");           
        }

        //[Route(nameof(CheckoutBook) + "/{userId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> CheckoutBook(string Id)//vm
        {
            if (Id == null )
            {
                ViewBag.ErrorTitle = $"You are tring to CheckOut a book with invalid model state!";
                ViewBag.ErrorMessage = "Book Id cannot be null!";
                return View("Error");
            }
            ClaimsPrincipal currUser = this.User;
            var userId = currUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var user = await  _userManager.GetUserAsync(User);

            var book = await _bookService.FindByIdAsync(Id); 
            if (book == null)
            {
                ViewBag.ErrorTitle = $"You are tring to CheckOut a book with invalid model state!";
                return View("Error");
            }
            if (book.Copies <= 0)
            {
                ViewBag.ErrorTitle = $"You are tring to CheckOut a book without copies available !";
                return View("Error");
            }
            var hr = await _historyService.CheckoutBookAsync(Id, userId);

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

        [Authorize(Roles = "Member")]
        public async Task<IActionResult> MyReservations()
        {
            var user = await _userManager.GetUserAsync(User);

            var reservations = await _reservationService.GetReservationsOfUser(user.Id);
            var reservedBooks = await _reservationService.ExtractBooksFromReservation(reservations);

            var reservationsVm = reservedBooks.Select(r=>r.MapToBookViewModel());

            return View(reservationsVm);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> ReturnBookAsync(string Id)
        {
            if (Id == null)
            {
                ViewBag.ErrorTitle = $"You are tring to Return a Book with invalid model state!";
                return View("Error");
            }
            var book = await _bookService.FindByIdAsync(Id);
            if (book == null)
            {
                ViewBag.ErrorTitle = $"You are tring to see Return  a book with invalid model state";
                return View("Error");
            }
            var checkForReservations = await _reservationService.CheckIfBookExistInReservations(Id);
            var user = await _userManager.GetUserAsync(User);

            if (checkForReservations == null)
                await _historyService.ReturnBookAsync(Id, user.Id);

            var title = await _bookService.GetBookTitleAsync(Id);
            var username = user.UserName;
            //notification for admin
            var returnNotificaitonMsg = _notificationManager.ReturnBookDescription(username, title);
            var notification = await _notificationService.CreateNotificationAsync(returnNotificaitonMsg, username);

            if (checkForReservations != null)
            {
                //notification for user , who is first from reservations! ( if there is one...)
                var userToNotify = checkForReservations.UserId;
                var usernameToNotify = await _userService.FindUsernameByIdAsync(userToNotify);
                var description = _notificationManager.BookWasGivenToUser(usernameToNotify, title);
                var notify = await _notificationService.SendNotificationToUserAsync(description, usernameToNotify);
                //notification for admin, that the book is transferd to another user
                var adminNotificationTransferMsg = _notificationManager.TransferBookDescription(username, usernameToNotify, title);
               await _notificationService.CreateNotificationAsync(adminNotificationTransferMsg, username);
            }


            _toast.AddSuccessToastMessage($"{username}, you successfully returned a book: \"{title}\"!");
            return RedirectToAction(nameof(MyBooks));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> RenewBookAsync(string Id)
        {
            if (Id == null)
            {
                ViewBag.ErrorTitle = $"You are tring to Renew a Book with invalid model state!";
                return View("Error");
            }

            var user = await _userManager.GetUserAsync(User);

            var book = await _bookService.FindByIdAsync(Id);
            if (book == null)
            {
                ViewBag.ErrorTitle = $"You are tring to see Reserve  a book with invalid model state";
                return View("Error");
            }
            var hr = await _historyService.RenewBookAsync(Id, user.Id);

            var title = await _bookService.GetBookTitleAsync(Id);
            var username = user.UserName;

            var notificationDescription = _notificationManager.RenewBookDescription(username, hr.ReturnDate, title);

            var notification = await _notificationService.CreateNotificationAsync(notificationDescription, username);

            _toast.AddSuccessToastMessage($"{username}, you successfully renew return date of a book: \"{title}\" to {hr.ReturnDate} !");

            return RedirectToAction(nameof(MyBooks));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> ReserveBook(string Id)
        {
            if (Id == null)
            {
                ViewBag.ErrorTitle = $"You are tring to Reserve a Book with invalid model state!";
                return View("Error");
            }

            var user = await _userManager.GetUserAsync(User);

            var book = await _bookService.FindByIdAsync(Id);
            if (book == null)
            {
                ViewBag.ErrorTitle = $"You are tring to see Reserve  a book with invalid model state";
                return View("Error");
            }
            var notification = await _reservationService.ReserveBookAsync(Id, user.Id);


            var title = await _bookService.GetBookTitleAsync(Id);
            var username = user.UserName;

            var notificationDescription = _notificationManager.ReserveBookDescription(username, title);

            await _notificationService.CreateNotificationAsync(notificationDescription, username);

            _toast.AddSuccessToastMessage($"{username}, you successfully reserve a book \"{title}\"!");
            return RedirectToAction(nameof(MyReservations));
        }

    }
}