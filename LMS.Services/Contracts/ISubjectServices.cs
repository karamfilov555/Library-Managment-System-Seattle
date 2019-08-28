using LMS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services.Contracts
{
    public interface ISubjectServices
    {
        void AddSubjectsToDb(ICollection<BookSubject> subjects);
        ICollection<BookSubject> ProvideSubject(string[] subjects);
    }
}
