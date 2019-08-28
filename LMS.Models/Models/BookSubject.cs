using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Models
{
    public class BookSubject
    {
        public BookSubject()
        {

        }
      
        public int BookId { get; set; }
        [Required]
        public Book Book { get; set; }
        public int SubjectCategoryId { get; set; }
        [Required]
        public SubjectCategory SubjectCategory { get; set; }

    }
}
