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
        Task<ICollection<Book>> GetAllBooksWithoutRepetitionsAsync();
        Task<Book> FindFreeBookByIdAsync(string id);
        Task<ICollection<Book>> GetAllBooksForAdminWithoutRepetitionsAsync();
        Task<string> GetBookTitleAsync(string bookId);
        Task<ICollection<Book>> GetAllSameBooks(string Id);
        Task<ICollection<Book>> GetUnavailableBooksWithoutRepetitions();
        Task<Book> GetBooksForHomePage();
        Task LockBook(string Id);
    }
}
