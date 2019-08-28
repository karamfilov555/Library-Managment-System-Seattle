using LMS.Models.Models;
using LMS.Services.ModelProviders.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services.ModelProviders
{
    public class IsbnFactory : IIsbnFactory
    {
        private readonly IIsbnGenerator _isbnGenerator;
        public IsbnFactory(IIsbnGenerator isbnGenerator)
        {
            _isbnGenerator = isbnGenerator;
        }
        public Isbn CreateIsbn()
        {
            var isbnCode = _isbnGenerator.GenerateISBN();
            var isbn = new Isbn(isbnCode);
            return isbn;
        }
    }
}
