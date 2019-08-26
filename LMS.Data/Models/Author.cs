using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Data.Models
{
    public class Author
    {
        internal Author()// ot ver 2.1 na ef moje bez prazniq konstr !
        {

        }
        internal Author(string name)
        {
            this.Name = name;
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public ICollection<Book> Books { get; set; }
    }
}
