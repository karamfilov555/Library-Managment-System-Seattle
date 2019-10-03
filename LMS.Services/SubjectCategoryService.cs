using LMS.Data;
using LMS.Models;
using LMS.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class SubjectCategoryService : ISubjectCategoryService
    {
        private readonly LMSContext _context;

        public SubjectCategoryService(LMSContext context)
        {
            _context = context;
        }
        private async Task<SubjectCategory> AddSubjectAsync(SubjectCategory subject)
        {
            await _context.SubjectCategories.AddAsync(subject);
            await _context.SaveChangesAsync();
            return subject;
        }
        private async Task<SubjectCategory> FindSubjectByNameAsync(string name)
        {
            var subjectToFind = await _context.SubjectCategories.FirstOrDefaultAsync(a => a.Name == name);
            return subjectToFind;
        }
        public async Task<SubjectCategory> ProvideSubjectAsync(string name)
        {
            if (!CheckIfSubjectExist(name))
            {
                var subject = new SubjectCategory { Name = name };
                await AddSubjectAsync(subject);
                return subject;
            }
            else
            {
                var author = await FindSubjectByNameAsync(name);
                return author;
            }
        }
        private bool CheckIfSubjectExist(string name)
        {
            return _context.SubjectCategories.Any(a => a.Name.ToLower() == name.ToLower());
        }
    }
}
