using LMS.Models;
using LMS.Services.ModelProviders.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.Services.ModelProviders
{
    public class SubjectFactory : ISubjectFactory
    {
        public SubjectFactory()
        {

        }
        public ICollection<BookSubject> CreateSubject(string[] subjects)
        {
            var bookSubjects = new List<BookSubject>();

            bookSubjects = subjects.
                 Select(subject => new BookSubject()
                 { SubjectCategory = new SubjectCategory(subject) })
                    .ToList();

            return bookSubjects;
        }
    }
}
