using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartBooking.Application.Dtos;
using SmartBooking.Application.Services.Doctors;
using SmartBooking.Application.Services.Specialities;
using SmartBooking.Core.Entities;
using SmartBooking.Core.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialitiesController(ISpecialityService specialityService) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecialityReadDto>>> GetAll() =>
            Ok(await specialityService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<SpecialityReadDto>> GetById(int id)
        {
            var speciality = await specialityService.GetByIdAsync(id);
            if (speciality == null)
                return NotFound("Speciality not found.");

            return Ok(speciality);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SpecialityCreateDto dto)
        {
            var speciality = await specialityService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = speciality.Id }, speciality);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SpecialityUpdateDto dto)
        {
            var isUpdated = await specialityService.UpdateAsync(id, dto);

            return !isUpdated ? NotFound("Speciality not found.") : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await specialityService.DeleteAsync(id);

            return !isDeleted ? NotFound("Speciality not found.") : NoContent();
        }
    }
}
