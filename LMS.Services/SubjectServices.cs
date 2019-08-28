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
        public void AddSubjectsToDb(ICollection<BookSubject> subjects)
        {
            foreach (var subject in subjects)
            {
                if (!_context.SubjectCategories.
                    Any(s => s.SubjectName == subject.SubjectCategory.SubjectName))
                {
                    _context.SubjectCategories.Add(subject.SubjectCategory);
                }
            }
            _context.SaveChanges();
        }
        public ICollection<BookSubject> ProvideSubject(string[] subjects)
        {
            var bookSubjects = _subjectFactory.CreateSubject(subjects);
            AddSubjectsToDb(bookSubjects);
            return bookSubjects;
        }
    }
}
