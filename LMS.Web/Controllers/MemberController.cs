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
        private readonly IHistoryService _historyService;
        private readonly IBookService _bookService;

        public MemberController(IHistoryService historyService, IBookService bookService)
        {
            _historyService = historyService;
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
            if (Id == null)
                return NotFound();
            ClaimsPrincipal currUser = this.User;
            var userId = currUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            var hr = await _historyService.CheckoutBook(Id,userId);
            var book = await _bookService.FindByIdAsync(Id);

            var chechoutBookVm = new CheckoutBookViewModel
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
            
            return View(chechoutBookVm);
        }
    }
}