using LMS.Generators.Contracts;
using System;
using System.Linq;

namespace LMS.Data.Models.ModelsFactory
{
    public class FactoryModels : IModelsFactory
    {
        // TUK MOJE DA SE SLOJI VALIDATORA I DA SE PRAVQT PROVERKITE !
        //private readonly ILoginAuthenticator _loginAuthenticator;
        private readonly IIsbnGenerator _isbnGenerator;
        private readonly LMSContext _context;
        public FactoryModels(/*ILoginAuthenticator loginAuthenticator,*/
            IIsbnGenerator isbnGenerator,
            LMSContext context)
        {
            //_loginAuthenticator = loginAuthenticator;
            _isbnGenerator = isbnGenerator;
            _context = context;
        }
        public User CreateUser(string username, string password, string roleName)
        {
            var role = CreateRole(roleName);

            //if (_context.User.Any(n => n.Username == username))
            //    throw new ArgumentException($"Username: {username} is taken.");

            var user = new User(username, password, role);
            return user;
        }
        public Book CreateBook(string title, string authorName, int pages, int year, string country, string language, string subjectName)
        {
            // some validations here
            var author = CreateAuthor(authorName);
            var subject = CreateSubject(subjectName);
            var isbn = _isbnGenerator.GenerateISBN();
            var book = new Book(title, author, pages, year, country, language, subject, isbn);
            return book;
        }
        public HistoryRegistry CreateRegistry(User user, Book book)
        {
            var registry = new HistoryRegistry(user, book);
            return registry;
        }
        public Author CreateAuthor(string name)
        {
            var author = new Author(name);
            return author;
        }
        public Role CreateRole(string name)
        {
            Role role;
            if (!_context.Role.Any(c => c.Name == name))
            {
                role = new Role(name);
                _context.Role.Add(role);
                _context.SaveChanges();
            }
            else
            {
                role = _context.Role.FirstOrDefault(c => c.Name == name);
            }

            return role;
        }
        public SubjectCategory CreateSubject(string name)
        {
            var subject = new SubjectCategory(name);
            return subject;
        }
    }
}
