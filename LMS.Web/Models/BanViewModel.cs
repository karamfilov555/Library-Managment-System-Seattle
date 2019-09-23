using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Web.Models
{
    public class BanViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Display(Name = "Ban Description")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Ban Expiration date")]
        [Required]
        public DateTime ExpirationDate { get; set; }
        [Required]
        public string Username { get; set; }
    }
}
