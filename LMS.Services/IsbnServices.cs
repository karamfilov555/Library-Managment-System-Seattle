using LMS.Data;
using LMS.Models.Models;
using LMS.Services.Contracts;
using LMS.Services.ModelProviders.Contracts;
using System.Linq;

namespace LMS.Services
{
    public class IsbnServices : IIsbnServices
    {
        private readonly IIsbnFactory _isbnFactory;
        private readonly LMSContext _context;

        public IsbnServices(LMSContext context,
                            IIsbnFactory isbnFactory)
        {
            _isbnFactory = isbnFactory;
            _context = context;
        }
        public Isbn ProvideIsbn()
        {
           return _isbnFactory.CreateIsbn();
        }
        public bool CheckIfIsbnExist(string isbn)
        {
            return _context.Isbns.Any(a => a.ISBN == isbn);
        }
    }
}
