using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class Author
    {
        public Author()
        {

        }
        public Author(string name)
        {
            this.Name = name;
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
