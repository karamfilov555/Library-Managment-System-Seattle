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

        public async Task<IActionResult> CheckoutBook(string bookId)
        {
            ClaimsPrincipal currUser = this.User;
            var currUserId = currUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var book = await _bookService.CheckoutBookAsync(bookId, currUserId);
            //var vm = new BookViewModel
            //{
            //    Year = book.Year,
            //    Pages = book.Pages,
            //    AuthorName = book.Author.Name,
            //    Title = book.Title,
            //}
            return View(book);
        }
    }
}