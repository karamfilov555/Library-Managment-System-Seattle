using LMS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Services.Contracts
{
    public interface IHistoryService
    {
        Task<HistoryRegistry> CheckoutBookAsync(string bookId, string userId);
        Task<IDictionary<Book, DateTime>> GetCheckOutsOfUserAsync(string userId);
        Task ReturnBookAsync(string bookId, string userId);
        Task<HistoryRegistry> RenewBookAsync(string bookId, string userId);
        Task AutoReturnAllBooksOfUser(string userId);
    }
}