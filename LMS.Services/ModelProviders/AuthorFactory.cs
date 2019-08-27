using LMS.Models;
using LMS.Services.ModelProviders.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services.ModelProviders
{
    public class AuthorFactory : IAuthorFactory
    {
        public AuthorFactory()
        {
        }
        public Author CreateAuthor(string name)
        {
            var author = new Author(name);
            return author;
        }
    }
}
