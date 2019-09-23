using LMS.Models;
using System.Threading.Tasks;

namespace LMS.Services.Contracts
{
    public interface IAuthorService
    {
        Task<Author> ProvideAuthorAsync(string name);
    }
}
