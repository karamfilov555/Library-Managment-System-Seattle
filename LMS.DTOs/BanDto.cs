using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.DTOs
{
    public class BanDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Username { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
