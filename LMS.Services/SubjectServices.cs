using LMS.Data;
using LMS.Models;
using LMS.Services.Contracts;
using LMS.Services.ModelProviders.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.Services
{
    public class SubjectServices : ISubjectServices
    {
        private readonly LMSContext _context;
        private readonly ISubjectFactory _subjectFactory;

        public SubjectServices(LMSContext context,
                               ISubjectFactory subjectFactory)
        {
            _context = context;
            _subjectFactory = subjectFactory;
        }
        public void AddSubjectToDb(SubjectCategory subj)
        {
            if (!_context.SubjectCategories.Any(s => s.SubjectName == subj.SubjectName))
            {
                _context.SubjectCategories.Add(subj);
                _context.SaveChanges();
            }
        }
        public ICollection<BookSubject> ProvideSubject(string[] subjects)
        {
            var bookSubject = new List<BookSubject>();

               bookSubject = subjects.
                    Select(subject => new BookSubject()
                      { SubjectCategory = new SubjectCategory(subject) })
                       .ToList();

            return bookSubject;
        }
    }
}
