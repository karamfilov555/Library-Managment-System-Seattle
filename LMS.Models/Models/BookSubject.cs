using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class BookSubject
    {
        public BookSubject()
        {

        }
        public string Id { get; set; }
        public string BookId { get; set; }
        [Required]
        public Book Book { get; set; }
        public string SubjectCategoryId { get; set; }
        [Required]
        public SubjectCategory SubjectCategory { get; set; }
    }
}
