using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Web.Models
{
    public class BookListViewModel
    {
        public string  Id { get; set; }
        [Display(Name = "Title")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Author")]
        [Required, RegularExpression("[A-Za-z ]+", ErrorMessage = "Author's name should only contain latin letters!")]
        public string AuthorName { get; set; }
        [Display(Name = "Pages")]
        [Required, Range(0, 100000, ErrorMessage = "Pages must be value between 0 and 100000")]
        public int Pages { get; set; }
        [Display(Name = "Year")]
        [Required, Range(-500, 2019, ErrorMessage = "Year must be value between -500 and 2019")]
        public int Year { get; set; }
        [Required, RegularExpression("[A-Za-z]+", ErrorMessage = "Country should only contain latin letters!")]
        public string Country { get; set; }
        [Required, RegularExpression("[A-Za-z]+", ErrorMessage = "Language should only contain latin letters!")]
        public string Language { get; set; }

        [Display(Name = "Copies")]
        [Required, Range(1, 100, ErrorMessage = "Copies must be value between 1 and 100")]
        public int Copies { get; set; }
        public bool IsLocked { get; set; }
        public bool IsCheckedOut { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string SubjectCategoryId { get; set; }
        [Display(Name = "Subject")]
        [Required, RegularExpression("[A-Za-z]+", ErrorMessage = "Subject should only contain latin letters!")]
        public string SubjectCategoryName { get; set; }

        [Display(Name = "Cover Image")]
        [Required]
        public string CoverImageUrl { get; set; }
        public decimal? Rating { get; set; }
    }
}
