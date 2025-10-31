using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBooking.Application.Dtos;
using SmartBooking.Application.Services.Restaurants;
using SmartBooking.Application.Services.Specialities;

namespace SmartBooking.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RestaurantsController(IRestaurantService restaurantService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantReadDto>>> GetAll() =>
            Ok(await restaurantService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<RestaurantReadDto>> GetById(int id)
    {
        var restaurant = await restaurantService.GetByIdAsync(id);
        if (restaurant == null)
            return NotFound("Restaurant not found.");

        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RestaurantDto dto)
    {
        var restaurant = await restaurantService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = restaurant.Id }, restaurant);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] RestaurantDto dto)
    {
        var isUpdated = await restaurantService.UpdateAsync(id, dto);

        return !isUpdated ? NotFound("Restaurant not found.") : NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var isDeleted = await restaurantService.DeleteAsync(id);

        return !isDeleted ? NotFound("Restaurant not found.") : NoContent();
    }
}
