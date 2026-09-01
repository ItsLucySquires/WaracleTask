namespace WaracleTask.Data.Models
{
    public class Booking
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set;  }
        public Room Room { get; set; }
    }
}
