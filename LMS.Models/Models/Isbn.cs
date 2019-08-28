using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Models.Models
{
    public class Isbn
    {
        public Isbn()
        {

        }
        public Isbn(string isbn)
        {
            this.ISBN = isbn;
        }
        public int Id { get; set; }

        public string ISBN { get; set; }
        //public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
