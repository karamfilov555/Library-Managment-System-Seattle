using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LMS.Models;

namespace LMS.Web.Controllers
{
    [Authorize(Roles = "Librarian")]
    [Route("Librarian")]
    public class LibrarianController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateBook()
        {
            //ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id");
            //ViewData["BookRatingId"] = new SelectList(_context.Set<BookRating>(), "Id", "Id");
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBook([Bind("Id,Title,AuthorId,Pages,Year,Country,Language,Copies,IsReserved,IsCheckedOut,BookRatingId,CoverImageUrl")] Book book)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Add(book);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Id", book.AuthorId);
            //ViewData["BookRatingId"] = new SelectList(_context.Set<BookRating>(), "Id", "Id", book.BookRatingId);
            return View(book);
        }
    }
}