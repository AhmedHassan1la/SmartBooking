using Microsoft.AspNetCore.Mvc;
using SmartBooking.Application.DTOs;
using SmartBooking.Core.Entities;
using SmartBooking.Core.Repositories.Interfaces;

namespace SmartBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;


    public ClinicsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/clinics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClinicReadDto>>> GetAllClinics()
        {
            var clinics = await _unitOfWork.Repository<Clinic>().GetAllAsync();
            var result = clinics.Select(c => new ClinicReadDto
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                PhoneNumber = c.PhoneNumber
            });

            return Ok(result);
        }

        // GET: api/clinics/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClinicReadDto>> GetClinicById(int id)
        {
            var clinic = await _unitOfWork.Repository<Clinic>().GetAsync(id);
            if (clinic == null)
                return NotFound();

            var result = new ClinicReadDto
            {
                Id = clinic.Id,
                Name = clinic.Name,
                Address = clinic.Address,
                PhoneNumber = clinic.PhoneNumber
            };

            return Ok(result);
        }

        // POST: api/clinics
        [HttpPost]
        public async Task<ActionResult<ClinicReadDto>> CreateClinic([FromBody] ClinicCreateDto dto)
        {
            var clinic = new Clinic
            {
                Name = dto.Name,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber
            };

            await _unitOfWork.Repository<Clinic>().AddAsync(clinic);
            await _unitOfWork.CompleteAsync();

            var result = new ClinicReadDto
            {
                Id = clinic.Id,
                Name = clinic.Name,
                Address = clinic.Address,
                PhoneNumber = clinic.PhoneNumber
            };

            return CreatedAtAction(nameof(GetClinicById), new { id = clinic.Id }, result);
        }

        // PUT: api/clinics/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateClinic(int id, [FromBody] ClinicUpdateDto dto)
        {
            var existingClinic = await _unitOfWork.Repository<Clinic>().GetAsync(id);
            if (existingClinic == null)
                return NotFound();

            existingClinic.Name = dto.Name;
            existingClinic.Address = dto.Address;
            existingClinic.PhoneNumber = dto.PhoneNumber;

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
