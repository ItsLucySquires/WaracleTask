using Microsoft.AspNetCore.Mvc;
using WaracleTask.Data.Models;
using WaracleTask.Services;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : Controller
{
    private readonly BookingService _bookingService;
    private readonly HotelsService _hotelService;

    public BookingsController(BookingService bookingService, HotelsService hotelService)
    {
        _bookingService = bookingService;
        _hotelService = hotelService;
    }

    /// <summary>
    /// Gets a list of all bookings
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var bookings = await _bookingService.GetAllBookings();
        return Ok(bookings);
    }

    /// <summary>
    /// Gets the details of a specific booking by its ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Details(System.Guid id)
    {
        var booking = await _bookingService.GetBooking(id);
        if (booking == null)
        {
            return NotFound();
        }
        return Ok(booking);
    }

    /// <summary>
    /// Finds available rooms for a hotel based on the number of guests and the start and end dates
    /// </summary>
    /// <param name="hotelId"></param>
    /// <param name="start"></param>
    /// <param name="finish"></param>
    /// <param name="guests"></param>
    /// <returns></returns>
    private async Task<List<Room>> GetAvailableHotelRooms(Guid hotelId, DateTime start, DateTime finish, int guests)
    {
        var hotel = await _hotelService.GetHotel(hotelId);
        if (hotel == null)
        {
            return new List<Room>();
        }
        //Find all rooms that have the amount of space for the number of guests that don't have bookings between the start and end dates
        var rooms = (guests < 2 ? hotel.Rooms.Where(x => x.Type == "Single") : hotel.Rooms.Where(x => x.Type == "Double" || x.Type == "Deluxe")).ToList();
        var bookedRooms = await _bookingService.GetBookedRooms(rooms, start, finish);
        rooms.RemoveAll(x => bookedRooms.Contains(x));
        return rooms;
    }

    /// <summary>
    /// Finds available rooms for a hotel based on the number of guests and the start and end dates
    /// </summary>
    /// <param name="hotelId"></param>
    /// <param name="start"></param>
    /// <param name="finish"></param>
    /// <param name="guests"></param>
    /// <returns></returns>
    [HttpGet("find-rooms")]
    public async Task<IActionResult> FindRooms(Guid hotelId, DateTime start, DateTime finish, int guests)
    {
        var getRooms = await GetAvailableHotelRooms(hotelId, start, finish, guests);
        if (getRooms.Any())
        {
            return Ok(getRooms);
        }
        return NotFound();
    }

    /// <summary>
    /// Creates a booking for a hotel room if available
    /// </summary>
    /// <param name="hotelId"></param>
    /// <param name="start"></param>
    /// <param name="finish"></param>
    /// <param name="guests"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<IActionResult> Create(Guid hotelId, DateTime start, DateTime finish, int guests)
    {
        if (start < finish)
        {
            var getRooms = await GetAvailableHotelRooms(hotelId, start, finish, guests);
            if (getRooms.Any())
            {
                var booking = new Booking()
                {
                    Id = new Guid(),
                    Start = start,
                    End = finish,
                    Room = getRooms.First()
                };
                await _bookingService.CreateBooking(booking);
                return Ok(booking);

            }
        }
        return BadRequest();
    }

}
