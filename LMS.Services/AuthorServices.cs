using LMS.Data;
using LMS.Models;
using LMS.Services.Contracts;
using LMS.Services.ModelProviders.Contracts;
using System.Linq;

namespace LMS.Services
{
    public class AuthorServices : IAuthorServices
    {
        private readonly LMSContext _context;
        private readonly IAuthorFactory _authorFactory;

        public AuthorServices(LMSContext context,
                              IAuthorFactory authorFactory)
        {
            _context = context;
            _authorFactory = authorFactory;
        }
        public bool CheckIfAuthorExist(string name)
        {
            if (!_context.Authors.Any(a => a.Name == name))
                return false;
            return true;
        }
        public Author AddAuthorToDb(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
            return author;
        }
        public Author FindAuthorByName(string name)
        {
            var authorToFind = _context.Authors.FirstOrDefault(a => a.Name == name);
            return authorToFind;
        }
        public Author ProvideAuthor(string name)
        {
            if (!CheckIfAuthorExist(name))
            {
                var author = _authorFactory.CreateAuthor(name);
                return author;
            }
            else
            {
                var author = FindAuthorByName(name);
                return author;
            }
        }
    }
}
