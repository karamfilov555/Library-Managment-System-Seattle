using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class HistoryRegistry
    {
        public HistoryRegistry()
        { }
  
        public string Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string BookId { get; set; }
        public Book Book { get; set; }
        [Required]
        public string CheckOutDate { get; set; }
        [Required]
        public DateTime ReturnDate { get; set; }
        [Required]
        public bool IsReturned { get; set; }
    }
}
