using System.ComponentModel.DataAnnotations;

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
