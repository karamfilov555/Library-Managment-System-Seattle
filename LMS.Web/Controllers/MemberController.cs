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
    public class MemberController : Controller
    {
        private readonly IBookService _bookService;

        public MemberController(IBookService bookService)
        {
            _bookService = bookService;
        }
        public IActionResult Index()
        {
            return View();
        }

        //[Route(nameof(CheckoutBook) + "/{userId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckoutBook(string Id)//vm
        {
            //ClaimsPrincipal currUser = this.User;
            //var currUserId = currUser.FindFirst(ClaimTypes.NameIdentifie  r).Value;
            //var book = await _bookService.CheckoutBookAsync(bookId, currUserId);
            ////var vm = new BookViewModel
            ////{
            ////    Year = book.Year,
            ////    Pages = book.Pages,
            //    AuthorName = book.Author.Name,
            //    Title = book.Title,
            //}
            //return View(book);
            //if (userId == null)
            //    return NotFound();

            var book = await _bookService.FindByIdAsync(Id);
            //var vm = book.MapToBookViewModel();
            //if (vm == null)
            //    return NotFound();

            return View(book);
            //var book = new BookViewModel();
            //return View(book);
        }
    }
}