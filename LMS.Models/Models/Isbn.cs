namespace LMS.Models
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
        public string Id { get; set; }
        public string ISBN { get; set; }
        public string BookId { get; set; }
        public Book Book { get; set; }
    }
}
