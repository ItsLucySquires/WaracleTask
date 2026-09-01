using Microsoft.EntityFrameworkCore;
using WaracleTask.Data.Models;

namespace WaracleTask.Services
{
    public class BookingService
    {
        private readonly WaracleTaskContext _context;

        public BookingService(WaracleTaskContext context)
        {
            _context = context;
        }

        public async Task<List<Booking>> GetAllBookings()
        {
            var booking = await _context.Booking.Include(x => x.Room).ToListAsync();
            return booking;
        }

        public async Task<Booking> GetBooking(Guid id)
        {
            var booking = await _context.Booking.Include(x => x.Room)
            .FirstOrDefaultAsync(m => m.Id == id);
            return booking;
        }

        /// <summary>
        /// Get all bookings for selected rooms in a time range
        /// </summary>
        /// <param name="rooms"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<List<Room>> GetBookedRooms(List<Room> rooms, DateTime start, DateTime end)
        {
            var booking = await _context.Booking.Where(x => rooms.Contains(x.Room) && x.Start >= start && x.End <= end).ToListAsync();
            var bookedRooms = booking.Select(x => x.Room).ToList();
            return bookedRooms;
        }

        public async Task CreateBooking(Booking booking)
        {

            _context.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBooking(Guid id)
        {

            var booking = await _context.Booking.FindAsync(id);
            if (booking != null)
            {
                _context.Booking.Remove(booking);
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateBooking(Booking booking)
        {
            _context.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAllBookings()
        {
            _context.RemoveRange(_context.Booking);
            await _context.SaveChangesAsync();
        }
    }
}
