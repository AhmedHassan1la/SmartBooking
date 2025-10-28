using Microsoft.AspNetCore.Mvc;
using SmartBooking.Application.Dtos;
using SmartBooking.Core.Entities;
using SmartBooking.Core.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialitiesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpecialitiesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // =======================================================
        // دوال التحويل (Manual Mapping)
        // =======================================================

        private SpecialityReadDto MapToReadDto(Speciality speciality)
        {
            if (speciality == null) return null;

            return new SpecialityReadDto
            {
                Id = speciality.Id,
                Name = speciality.Name,
                Description = speciality.Description
            };
        }

        private Speciality MapToEntity(SpecialityCreateDto dto)
        {
            return new Speciality
            {
                Name = dto.Name,
                Description = dto.Description
            };
        }

    

        // GET: api/specialities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecialityReadDto>>> GetAll()
        {
            var specialities = await _unitOfWork.Repository<Speciality>().GetAllAsync();
            var result = specialities.Select(MapToReadDto).ToList();
            return Ok(result);
        }

        // GET: api/specialities/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SpecialityReadDto>> GetById(int id)
        {
            var speciality = await _unitOfWork.Repository<Speciality>().GetAsync(id);
            if (speciality == null)
                return NotFound("Speciality not found.");

            return Ok(MapToReadDto(speciality));
        }

        // POST: api/specialities
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SpecialityCreateDto dto)
        {
            var speciality = MapToEntity(dto);
            await _unitOfWork.Repository<Speciality>().AddAsync(speciality);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetById), new { id = speciality.Id }, MapToReadDto(speciality));
        }

        // PUT: api/specialities/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] SpecialityUpdateDto dto)
        {
            var existing = await _unitOfWork.Repository<Speciality>().GetAsync(id);
            if (existing == null)
                return NotFound("Speciality not found for update.");

            existing.Name = dto.Name;
            existing.Description = dto.Description;

            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        // DELETE: api/specialities/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var speciality = await _unitOfWork.Repository<Speciality>().GetAsync(id);
            if (speciality == null)
                return NotFound("Speciality not found for deletion.");

            await _unitOfWork.Repository<Speciality>().DeleteAsync(speciality);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
