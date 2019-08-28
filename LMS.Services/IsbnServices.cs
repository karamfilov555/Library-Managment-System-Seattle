using LMS.Data;
using LMS.Models.Models;
using LMS.Services.Contracts;
using LMS.Services.ModelProviders.Contracts;


namespace LMS.Services
{
    public class IsbnServices : IIsbnServices
    {
        private readonly IIsbnFactory _isbnFactory;

        public IsbnServices(LMSContext context,
                            IIsbnFactory isbnFactory)
        {
            _isbnFactory = isbnFactory;
        }
        public Isbn ProvideIsbn()
        {
           return _isbnFactory.CreateIsbn();
        }
    }
}
