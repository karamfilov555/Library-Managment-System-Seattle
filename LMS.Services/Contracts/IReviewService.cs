using System.Threading.Tasks;
using LMS.Models.Models;

namespace LMS.Services.Contracts
{
    public interface IReviewService
    {
        Task<Review> CreateReviewAsync(string userId, decimal grade, string description, string bookId);
    }
}