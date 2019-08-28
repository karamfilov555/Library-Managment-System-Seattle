namespace LMS.Models
{
    public class RecordFines
    {
        public RecordFines()
        {

        }
        public RecordFines(decimal money)
        {
            this.FineAmount = money;
        }
        public int Id { get; set; }
        public User User { get; set; }
        public decimal FineAmount { get; set; }
    }
}
