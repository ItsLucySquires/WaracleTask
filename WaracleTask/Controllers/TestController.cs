using Microsoft.AspNetCore.Mvc;
using WaracleTask.Data.Models;
using WaracleTask.Services;

namespace WaracleTask.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly BookingService _bookingService;
        private readonly HotelsService _hotelService;
        private readonly RoomsService _roomService;

        public TestController(BookingService bookingService, HotelsService hotelService, RoomsService roomService)
        {
            _bookingService = bookingService;
            _hotelService = hotelService;
            _roomService = roomService;
        }

        /// <summary>
        /// Seeds the database with sample data for testing purposes. This action creates three hotels and three bookings. This action is intended for testing purposes only and should not be used in a production environment.
        /// </summary>
        /// <returns></returns>
        [HttpGet("seed")]
        public async Task<IActionResult> SeedDatabase()
        {
            var hotel1Id = new Guid();
            var hotel2Id = new Guid();
            var hotel3Id = new Guid();
            var hotel1 = await _hotelService.CreateHotel(new Hotel() { Id = hotel1Id, Name = "Hotel Edinburgh" });
            var hotel2 = await _hotelService.CreateHotel(new Hotel() { Id = hotel2Id, Name = "Hotel Glasgow" });
            var hotel3 = await _hotelService.CreateHotel(new Hotel() { Id = hotel3Id, Name = "Hotel St Andrews" });
            var edinburgh = await _hotelService.GetHotel(hotel1.Id);
            var ediRoom1 = edinburgh.Rooms.First(x=>x.Type=="Single");
            var ediRoom2 = edinburgh.Rooms.First(x => x.Type == "Double");
            await _bookingService.CreateBooking(new Booking() { Id = new Guid(), Room = ediRoom1, Start = DateTime.UtcNow.AddDays(1), End = DateTime.UtcNow.AddDays(3) });
            await _bookingService.CreateBooking(new Booking() { Id = new Guid(), Room = ediRoom2, Start = DateTime.UtcNow.AddDays(5), End = DateTime.UtcNow.AddDays(7) });
            return Ok();
        }

        /// <summary>
        /// Resets the database by deleting all bookings, rooms, and hotels. This action is intended for testing purposes only and should not be used in a production environment.
        /// </summary>
        /// <returns></returns>

        [HttpGet("reset")]
        public async Task<IActionResult> ResetDatabase()
        {
            await _bookingService.DeleteAllBookings();
            await _roomService.DeleteAllRooms();
            await _hotelService.DeleteAllHotels();

            return Ok();
        }
    }
}
