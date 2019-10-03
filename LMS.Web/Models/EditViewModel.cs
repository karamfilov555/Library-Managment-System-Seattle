using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Web.Models
{
    public class EditViewModel
    {
        public string Id { get; set; }
        //validations!!!!!!!
        [Display(Name = "Book's Title")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Author's Name")]
        [Required, RegularExpression("[A-Za-z ]+", ErrorMessage = "Author's name should only contain latin letters!")]
        public string AuthorName { get; set; }
        [Display(Name = "Pages")]
        [Required, Range(0, 100000, ErrorMessage = "Pages must be value between 0 and 100000")]
        public int Pages { get; set; }
        [Display(Name = "Year of writing")]
        [Required, Range(-500, 2019, ErrorMessage = "Year must be value between -500 and 2019")]
        public int Year { get; set; }
        [Required, RegularExpression("[A-Za-z ]+", ErrorMessage = "Country should only contain latin letters!")]
        public string Country { get; set; }
        [Required, RegularExpression("[A-Za-z]+", ErrorMessage = "Language should only contain latin letters!")]
        public string Language { get; set; }

        [Display(Name = "Cover Image Url")]
        [Required]
        public string CoverImageUrl { get; set; }
    }
}
