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
        public Book Book { get; set; }
    }
}
