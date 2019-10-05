using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Web.Models
{
    public class AdvancedSearchViewModel
    {
        [RegularExpression("[A-Za-z ]+", ErrorMessage = "Title should only contain latin letters!")]
        public string Title { get; set; }
        [RegularExpression("[A-Za-z ]+", ErrorMessage = "Author's name should only contain latin letters!")]
        public string Author { get; set; }
        [RegularExpression("[A-Za-z ]+", ErrorMessage = "Title should only contain latin letters!")]
        public string Subject { get; set; }
        [Range(1,2022,ErrorMessage ="Must be a value between 1 and 2022")]
        public int Year { get; set; }
        public bool Inclusive { get; set; }
    }
}
