using System.ComponentModel.DataAnnotations;

namespace LMS.Web.Models
{
    public class MembershipViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Username { get; set; }
    }
}
