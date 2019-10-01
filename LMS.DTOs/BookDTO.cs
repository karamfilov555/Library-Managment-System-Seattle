using System;
using System.ComponentModel.DataAnnotations;

namespace LMS.DTOs
{
    public class BookDTO
    {
        public string Id { get; set; }
        //validations!!!!!!!
        [Required]
        public string Title { get; set; }

        [Required]
        public string AuthorName { get; set; }
        [Required]
        public int Pages { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public int Copies { get; set; }
      
        [Required]
        public string SubjectCategoryName { get; set; }

        public string CoverImageUrl { get; set; }
        public decimal? Rating { get; set; }
    }
}
