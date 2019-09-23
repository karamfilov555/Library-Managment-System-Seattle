using LMS.DTOs;
using LMS.Models;
using System.Threading.Tasks;

namespace LMS.Services.Contracts
{
    public interface IBookService
    {
        Task<Book> ProvideBookAsync(BookDTO bookDto);
        Task<Book> FindByIdAsync(string id);
    }
}
