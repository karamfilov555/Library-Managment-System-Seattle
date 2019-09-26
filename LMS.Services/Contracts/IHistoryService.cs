using LMS.Models;
using System.Threading.Tasks;

namespace LMS.Services.Contracts
{
    public interface IHistoryService
    {
        Task<HistoryRegistry> CheckoutBook(string bookId, string userId);
    }
}