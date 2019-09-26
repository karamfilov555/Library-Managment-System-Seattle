using LMS.DTOs;
using LMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Services.Contracts
{
    public interface IBookService
    {
        Task<Book> ProvideBookAsync(BookDTO bookDto);
        Task<Book> FindByIdAsync(string id);
        Task<ICollection<Book>> GetAllBooksAsync();
        Task<ICollection<Book>> GetAllBooksWithoutRepetitionsAsync();
        Task<Book> FindFreeBookByIdAsync(string id);
        //Task<Book> CheckoutBookAsync(string bookId, string userId);
    }
}
