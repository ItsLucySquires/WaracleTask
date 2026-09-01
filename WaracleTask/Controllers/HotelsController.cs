using Microsoft.AspNetCore.Mvc;
using WaracleTask.Data.Models;
using WaracleTask.Services;

[ApiController]
[Route("api/[controller]")]
public class HotelsController : Controller
{
    private readonly HotelsService _hotelService;

    public HotelsController(HotelsService hotelService)
    {
        _hotelService = hotelService;
    }

    /// <summary>
    /// Gets a list of all hotels
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<Hotel>> Index()    
    {
        return await _hotelService.GetHotels();
    }

    /// <summary>
    /// Obtain a list of hotels by a given name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet("name-search")]
    public async Task<List<Hotel>> GetHotelByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return new List<Hotel>();
        }
        var hotels = await _hotelService.GetHotelByName(name);
        return hotels;
    }


    /// <summary>
    /// Creates a new hotel
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<IActionResult> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return BadRequest("Hotel name cannot be empty.");
        }
        var hotel = new Hotel { Name = name };
        await _hotelService.CreateHotel(hotel);
        return Ok();
    }
}
