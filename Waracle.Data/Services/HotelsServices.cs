using Microsoft.EntityFrameworkCore;
using WaracleTask.Data.Models;

namespace WaracleTask.Services
{
    public class HotelsService
    {
        private readonly WaracleTaskContext _context;

        public HotelsService(WaracleTaskContext context)
        {
            _context = context;
        }

        public async Task<Hotel> GetHotel(Guid id)
        {
            var hotel = await _context.Hotel.Include(x => x.Rooms)
                .FirstOrDefaultAsync(m => m.Id == id);
            return hotel;
        }

        public async Task<List<Hotel>> GetHotels()
        {
            var hotels = await _context.Hotel
                .Include(h => h.Rooms)
                .ToListAsync();
            return hotels;
        }

        public async Task<List<Hotel>> GetHotelByName(string name)
        {
            var hotel = await _context.Hotel.Where(x => x.Name.ToLower() == name.ToLower()).Include(x => x.Rooms).ToListAsync();
            return hotel;
        }

        public async Task<Hotel> CreateHotel(Hotel hotel)
        {
            hotel.Rooms = new List<Room>();
            hotel.Rooms.Add(new Room() { Id = new Guid(), Type = "Single"});
            hotel.Rooms.Add(new Room() { Id = new Guid(), Type = "Single"});
            hotel.Rooms.Add(new Room() { Id = new Guid(), Type = "Double" });
            hotel.Rooms.Add(new Room() { Id = new Guid(), Type = "Double" });
            hotel.Rooms.Add(new Room() { Id = new Guid(), Type = "Deluxe" });
            hotel.Rooms.Add(new Room() { Id = new Guid(), Type = "Deluxe" });
            _context.Add(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task DeleteHotel(Guid id)
        {

            var hotel = await _context.Hotel.FindAsync(id);
            if (hotel != null)
            {
                _context.Hotel.Remove(hotel);
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateHotel(Hotel hotel)
        {
            _context.Update(hotel);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAllHotels()
        {
            _context.RemoveRange(_context.Hotel);
            await _context.SaveChangesAsync();
        }
    }
}
