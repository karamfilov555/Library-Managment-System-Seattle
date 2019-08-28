using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Models
{
    public class SubjectCategory
    {
        public SubjectCategory()
        {

        }
        
        public int Id { get; set; }

        public string SubjectName { get; set; }

        public  ICollection<BookSubject> BookSubject { get; set; }
            = new List<BookSubject>();
    }
}
