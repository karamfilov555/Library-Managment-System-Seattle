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
        [Required]
        public string Id { get; set; }
        [Required]
        [RegularExpression("A-Za-z")]
        public string Name { get; set; }

        public  ICollection<Book> BookSubject { get; set; } = new List<Book>();
    }
}
