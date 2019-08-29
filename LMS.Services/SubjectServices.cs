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
        public bool CheckIfSubjectExist(string name)
        {
            return _context.SubjectCategories.Any(a => a.SubjectName == name);
        }
        public BookSubject FindSubjectByName(string name)
        {
            var subjectCategory = _context.BooksSubjects
                                            .FirstOrDefault
                                            (a => a.SubjectCategory.SubjectName == name);
            return subjectCategory;
        }
        public ICollection<BookSubject> ProvideSubject(string[] subjects)
        {
            var bookSubjects = new List<BookSubject>();

            foreach (var subject in subjects)
            {
                //    if (!CheckIfSubjectExist(subject))
                //    {
                var subjectCategory = _subjectFactory.CreateSubject(subject);
                    //AddAuthorToDb(author);
                    bookSubjects.Add(subjectCategory);
                //}
                //else
                //{
                //    var subjectCategory = FindSubjectByName(subject);
                //    bookSubjects.Add(subjectCategory);
                //}
            }
            return bookSubjects;
        }
    }
}
