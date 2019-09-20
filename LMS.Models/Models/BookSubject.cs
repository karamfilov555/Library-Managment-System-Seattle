using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class BookSubject
    {
        public BookSubject()
        {

        }
        public string Id { get; set; }
        public int BookId { get; set; }
        [Required]
        public Book Book { get; set; }
        public int SubjectCategoryId { get; set; }
        [Required]
        public SubjectCategory SubjectCategory { get; set; }
    }
}
