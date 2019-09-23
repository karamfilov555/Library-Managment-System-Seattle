using LMS.Models;
using System.Threading.Tasks;

namespace LMS.Services.Contracts
{
    public interface ISubjectCategoryService
    {
        Task<SubjectCategory> ProvideSubjectAsync(string name);
    }
}
