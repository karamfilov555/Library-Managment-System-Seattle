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
        private readonly IBookService bookService;

        public ReviewService(LMSContext context, IBookService bookService)
        {
            _context = context;
            this.bookService = bookService;
        }

        public bool CheckIfUserCanReview(string userId, string bookId)
        {
            //var res = _context.Books.Where(r => r.Title == title);
            //foreach (var item in res)
            //{
            //    item.BookRating =
            //}
            var result = _context.Review.Any(r => r.UserId == userId && r.BookRating.BookId == bookId);
            return result;
        }
        public async Task CreateReviewAsync(string userId, decimal grade, string description, string bookId)
        {
            var book = bookService.GetBookTitleAsync(bookId);
            var allBooksWithSameTitle = await bookService.GetAllSameBooks(bookId);
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
                };
                await _context.Review.AddAsync(review);
                await _context.SaveChangesAsync();
            }
        }
    }
}
