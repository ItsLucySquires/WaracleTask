namespace WaracleTask.Data.Models
{
    public class Hotel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Room> Rooms { get; set; }

    }
}
