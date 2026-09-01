using Microsoft.EntityFrameworkCore;
using WaracleTask.Data.Models;

namespace WaracleTask.Services
{
    public class RoomsService
    {
        private readonly WaracleTaskContext _context;

        public RoomsService(WaracleTaskContext context)
        {
            _context = context;
        }

        public async Task<Room> GetRoom(Guid id)
        {
            var room = await _context.Room
            .FirstOrDefaultAsync(m => m.Id == id);
            return room;
        }

        public async Task CreateRoom(Room room)
        {
            _context.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoom(Guid id)
        {

            var room = await _context.Room.FindAsync(id);
            if (room != null)
            {
                _context.Room.Remove(room);
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoom(Room room)
        {
            _context.Update(room);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAllRooms()
        {
            _context.RemoveRange(_context.Room);
            await _context.SaveChangesAsync();
        }
    }
}
