using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartBooking.Application.DTOs;
using SmartBooking.Core.Entities;
using SmartBooking.Core.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClinicsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/clinics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClinicReadDto>>> GetAllClinics()
        {
            var clinics = await _unitOfWork.Repository<Clinic>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<ClinicReadDto>>(clinics);
            return Ok(result);
        }

        // GET: api/clinics/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClinicReadDto>> GetClinicById(int id)
        {
            var clinic = await _unitOfWork.Repository<Clinic>().GetAsync(id);
            if (clinic == null)
                return NotFound();

            var result = _mapper.Map<ClinicReadDto>(clinic);
            return Ok(result);
        }

        // POST: api/clinics
        [HttpPost]
        public async Task<ActionResult<ClinicReadDto>> CreateClinic([FromBody] ClinicCreateDto dto)
        {
            var clinic = _mapper.Map<Clinic>(dto);

            await _unitOfWork.Repository<Clinic>().AddAsync(clinic);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<ClinicReadDto>(clinic);

            return CreatedAtAction(nameof(GetClinicById), new { id = clinic.Id }, result);
        }

        // PUT: api/clinics/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateClinic(int id, [FromBody] ClinicUpdateDto dto)
        {
            var existingClinic = await _unitOfWork.Repository<Clinic>().GetAsync(id);
            if (existingClinic == null)
                return NotFound();

            _mapper.Map(dto, existingClinic);

            await _unitOfWork.Repository<Clinic>().UpdateAsync(id, existingClinic);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // DELETE: api/clinics/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteClinic(int id)
        {
            var clinic = await _unitOfWork.Repository<Clinic>().GetAsync(id);
            if (clinic == null)
                return NotFound();

            await _unitOfWork.Repository<Clinic>().DeleteAsync(clinic);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
