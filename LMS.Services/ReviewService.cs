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

        public ReviewService(LMSContext context)
        {
            _context = context;
        }

        public bool CheckIfUserCanReview(string userId, string bookId)
        {
            var result = _context.Review.Any(r => r.UserId == userId && r.BookRatingId == bookId);
            return result;
        }
        public async Task<Review> CreateReviewAsync(string userId, decimal grade, string description, string bookId)
        {
            BookRating bookRating = _context.BookRating.FirstOrDefault(br => br.BookId == bookId);
            if (bookRating!=null)
            {
                bookRating.Rating = grade;
            }
            else {
                bookRating = new BookRating
                {
                    BookId = bookId,
                    Rating= grade
                };
                _context.BookRating.Add(bookRating);
            }

            var review = new Review
            {
                Description = description,
                Grade = grade,
                UserId = userId,
            };
            await _context.Review.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }
    }
}
