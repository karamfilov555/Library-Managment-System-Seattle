using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LMS.Data;
using LMS.Models;
using LMS.Models.Models;
using Microsoft.AspNetCore.Authorization;
using LMS.Web.Models;
using LMS.Services.Contracts;
using LMS.Web.Mappers.Contracts;
using LMS.Web.Mappers;
using System;
using System.Collections.Generic;
using LMS.Web.PaginationManager;
using NToastNotify;

namespace LMS.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IMapVmToDTO _mapper;
        private readonly IToastNotification _toast;

        public BookController(IBookService book,
                              IMapVmToDTO mapper,
                              IToastNotification toast)
        {
            _bookService = book;
            _mapper = mapper;
            _toast = toast;
        }

        //[Authorize(Roles = "Librarian, Admin, Member")]
        [Authorize]
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            ICollection<Book> allBooks;
            ViewData["CurrentSort"] = sortOrder; //care
            ViewData["TitleSortCriteria"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["AuthornameSortCriteria"] = sortOrder == "authorname" ? "authorname_desc" : "authorname";
            ViewData["PagesSortCriteria"] = sortOrder == "pages" ? "pages_desc" : "pages";
            ViewData["YearSortCriteria"] = sortOrder == "year" ? "year_desc" : "year";
            ViewData["CountrySortCriteria"] = sortOrder == "country" ? "country_desc" : "country";
            ViewData["LanguageSortCriteria"] = sortOrder == "language" ? "language_desc" : "language";
            ViewData["RatingSortCriteria"] = sortOrder == "rating" ? "rating_desc" : "rating";
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
                pageNumber = 1;
            else
                searchString = currentFilter;

            if (User.IsInRole("Admin"))
                allBooks = await _bookService.GetAllBooksForAdminWithoutRepetitionsAsync();
            else
                allBooks = await _bookService.GetAllBooksWithoutRepetitionsAsync();

            var allBooksListVm = allBooks.Select(v => v.MapToListItemBookViewModel());


            switch (sortOrder)
            {
                case "title_desc":
                    allBooksListVm = allBooksListVm.OrderByDescending(b => b.Title);
                    break;
                case "authorname":
                    allBooksListVm = allBooksListVm.OrderBy(b => b.AuthorName);
                    break;
                case "authorname_desc":
                    allBooksListVm = allBooksListVm.OrderByDescending(s => s.AuthorName);
                    break;
                case "pages_desc":
                    allBooksListVm = allBooksListVm.OrderByDescending(b => b.Pages);
                    break;
                case "pages":
                    allBooksListVm = allBooksListVm.OrderBy(b => b.Pages);
                    break;
                case "year":
                    allBooksListVm = allBooksListVm.OrderBy(b => b.Year);
                    break;
                case "year_desc":
                    allBooksListVm = allBooksListVm.OrderByDescending(b => b.Year);
                    break;
                case "country":
                    allBooksListVm = allBooksListVm.OrderBy(b => b.Country);
                    break;
                case "country_desc":
                    allBooksListVm = allBooksListVm.OrderByDescending(b => b.Country);
                    break;
                case "language":
                    allBooksListVm = allBooksListVm.OrderBy(b => b.Country);
                    break;
                case "language_desc":
                    allBooksListVm = allBooksListVm.OrderByDescending(b => b.Country);
                    break;
                case "rating":
                    allBooksListVm = allBooksListVm.OrderBy(b => b.Rating);
                    break;
                case "rating_desc":
                    allBooksListVm = allBooksListVm.OrderByDescending(b => b.Rating);
                    break;

                default:
                    allBooksListVm = allBooksListVm.OrderBy(s => s.Title);
                    break;
            }
            var listVm = allBooksListVm.ToList();
            var unavailableBooks = await _bookService.GetUnavailableBooksWithoutRepetitions();
            var unavailableBooksVm = unavailableBooks.Select(b => b.MapToListItemBookViewModel());
            listVm.AddRange(unavailableBooksVm);

            if (!String.IsNullOrEmpty(searchString))
            {
                listVm = listVm.Where(bookVm => bookVm.Title.ToLower().Contains(searchString.ToLower()) || bookVm.AuthorName.ToLower().Contains(searchString.ToLower())).ToList();
            }

            var booksQuery = listVm.AsQueryable();

            int pageSize = 5;

            return View(PaginatedList<BookListViewModel>.CreateAsync(booksQuery.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        // GET: Book/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                ViewBag.ErrorTitle = $"You are tring to see Details of a book with Id = null";
                ViewBag.ErrorMessage = $"Book's Id cannot be null!";
                return View("Error");
            }

            var book = await _bookService.FindByIdAsync(id);
            var vm = book.MapToBookViewModel();
            if (vm == null)
                return NotFound();

            return View(vm);
        }
        public async Task<IActionResult> AdvancedSearch()
        {
            var searchVm = new AdvancedSearchViewModel();
            return View(searchVm);
        }
        public async Task<IActionResult> SearchResults(AdvancedSearchViewModel searchVm)
        {
            var results = await _bookService.GetFilteredResults(searchVm.Title, searchVm.Author, searchVm.Subject, searchVm.Year, searchVm.Inclusive);
            var booksVm = results.Select(b => b.MapToListItemBookViewModel());
            var booksQuery = booksVm.AsQueryable();

            int pageSize = 5;

            return View(PaginatedList<BookListViewModel>.CreateAsync(booksQuery.AsNoTracking(), 1 , pageSize));
        }
        
        [Authorize(Roles = "Librarian , Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Librarian, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookViewModel bookViewModel)
        {
            if (ModelState.IsValid)
            {
                var bookDto = await _mapper.MapBookVmToDTO(bookViewModel);
                await _bookService.ProvideBookAsync(bookDto);

                return RedirectToAction(nameof(Index));
            }
            ViewBag.ErrorTitle = $"You are tring to Create a book with invalid state!";
            return View("Error");
        }
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> LockBook(string Id)
        {
            if (Id == null)
            {
                ViewBag.ErrorTitle = $"You are tring to Lock a book with invalid state!";
                return View("Error");
            }
            await _bookService.LockBook(Id);

            _toast.AddSuccessToastMessage("You successfully lock a book!");
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnlockBook(string Id)
        {
            if (Id == null)
            {
                ViewBag.ErrorTitle = $"You are tring to Unlock a book with invalid state!";
                return View("Error");
            }
            await _bookService.UnlockBook(Id);
            _toast.AddSuccessToastMessage("You successfully unlock a book!");
            return RedirectToAction(nameof(Index));
        }
       
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                ViewBag.ErrorTitle = $"You are tring to Delete a book with invalid state!";
                return View("Error");
            }
            var book = await _bookService.FindByIdAsync(id);
            var bookVm = book.MapToBookViewModel();
            if (book == null)
            {
                ViewBag.ErrorTitle = $"You are tring to Delete a book with invalid state!";
                return View("Error");
            }

            return View(bookVm);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id == null)
            {
                ViewBag.ErrorTitle = $"You are tring to Delete a book with invalid state!";
                return View("Error");
            }
            await _bookService.DeleteBook(id);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
