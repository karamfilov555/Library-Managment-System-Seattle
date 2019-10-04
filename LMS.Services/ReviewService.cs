using LMS.Data;
using LMS.Models.Models;
using LMS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class ReviewService : IReviewService
    {
        private readonly LMSContext _context;
        private readonly IBookService _bookService;
        private readonly IUserService _userService;

        public ReviewService(LMSContext context, IBookService bookService, IUserService userService)
        {
            _context = context;
            _bookService = bookService;
            _userService = userService;
        }

        public async Task<bool> CheckIfUserCanReview(string userId, string bookId)
        {
            //var sameBooks = await bookService.GetAllSameBooks(bookId);
            var bookTitle = await _bookService.GetBookTitleAsync(bookId);
            
            var reviewsOfThisUser = _context.Review.Where(r => r.UserId == userId);
            foreach (var item in reviewsOfThisUser)
            {
                var bookRating = await _context.BookRating.FindAsync(item.BookRatingId);
                var bookTitlesRated =  _context.Books.Where(b => b.Id == bookRating.BookId);
                if (bookTitlesRated.Any(b=>b.Title == bookTitle))
                {
                    return false;
                }
            }
            return true;
        }
        public async Task CreateReviewAsync(string userId, decimal grade, string description, string bookId)
        {
            var bookTitle = await _bookService.GetBookTitleAsync(bookId);
            var allBooksWithSameTitle = await _bookService.GetAllSameBooks(bookId);
            //BookRating item.BookRating = _context.BookRating.Where(br => br.BookId == bookId);
            foreach (var item in allBooksWithSameTitle)
            {

                if (item.BookRating != null)
                {
                    item.BookRating.Rating = grade;
                    _context.Update(item);
                   await _context.SaveChangesAsync();
                }
                else
                {
                    var bookRating = new BookRating
                    {
                        BookId = bookId,
                        Rating = grade
                    };
                    _context.BookRating.Add(bookRating);
                    await _context.SaveChangesAsync();
                    item.BookRatingId = bookRating.Id;
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }

                var review = new Review
                {
                    BookRatingId = item.BookRating.Id,
                    Description = description,
                    Grade = grade,
                    UserId = userId,
                    BookTitle = bookTitle
                };
                 _context.Review.Add(review);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IDictionary<string,string>> GetAllCommentsForBook(string title)
        {
            var reviews = _context.Review.Where(b => b.BookTitle == title);
            var comments = new Dictionary<string,string>();
            foreach (var item in reviews)
            {
                var username = await _userService.FindUsernameByIdAsync(item.UserId);
                comments.Add(username, item.Description);
            }
            return comments;
        }
    }
}
