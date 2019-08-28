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
        public BookSubject CreateSubject(string subject)
        {
            return new BookSubject()
            {
                SubjectCategory = new SubjectCategory()
                {
                    SubjectName = subject
                }
            };
        }
    }
}
