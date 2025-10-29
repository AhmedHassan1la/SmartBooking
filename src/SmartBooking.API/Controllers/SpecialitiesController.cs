using AutoMapper;
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
        private readonly IMapper _mapper;

        public SpecialitiesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/specialities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecialityReadDto>>> GetAll()
        {
            var specialities = await _unitOfWork.Repository<Speciality>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<SpecialityReadDto>>(specialities);
            return Ok(result);
        }

        // GET: api/specialities/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SpecialityReadDto>> GetById(int id)
        {
            var speciality = await _unitOfWork.Repository<Speciality>().GetAsync(id);
            if (speciality == null)
                return NotFound("Speciality not found.");

            var result = _mapper.Map<SpecialityReadDto>(speciality);
            return Ok(result);
        }

        // POST: api/specialities
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SpecialityCreateDto dto)
        {
            var speciality = _mapper.Map<Speciality>(dto);

            await _unitOfWork.Repository<Speciality>().AddAsync(speciality);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<SpecialityReadDto>(speciality);
            return CreatedAtAction(nameof(GetById), new { id = speciality.Id }, result);
        }

        // PUT: api/specialities/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] SpecialityUpdateDto dto)
        {
            var existing = await _unitOfWork.Repository<Speciality>().GetAsync(id);
            if (existing == null)
                return NotFound("Speciality not found for update.");

            _mapper.Map(dto, existing);

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
