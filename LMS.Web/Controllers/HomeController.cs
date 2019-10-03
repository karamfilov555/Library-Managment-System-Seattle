using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LMS.Web.Models;
using LMS.Services.Contracts;
using LMS.Web.Mappers;

namespace LMS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _bookService;

        public HomeController(IBookService bookService)
        {
            _bookService = bookService;
        }
        public async Task<IActionResult> Index()
        {
            var booksForhomePage = await _bookService.GetBooksForHomePage();
            var bookVm = booksForhomePage.Select(b=>b.MapToBookViewModel()).ToList();
            return View(bookVm);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
