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
        var hotels = await _hotelService.GetHotelByName(name);
        return hotels;
    }


    // POST: HOTELS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost("create")]
    public async Task<IActionResult> Create(Hotel hotel)
    {
        await _hotelService.CreateHotel(hotel);
        return Ok();
    }
}
