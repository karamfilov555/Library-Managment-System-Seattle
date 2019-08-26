using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Data.Models
{
    public class SubjectCategory
    {
        internal SubjectCategory()
        {

        }
        internal SubjectCategory(string name)
        {
            this.SubjectName = name;
        }
        public int Id { get; set; }

        [Required]
        public string SubjectName { get; set; }

        [Required]
        public ICollection<Book> Books { get; set; }
    }
}
