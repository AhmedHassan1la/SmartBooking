using Microsoft.AspNetCore.Mvc;
using SmartBooking.Application.Dtos;
using SmartBooking.Core.Entities;
using SmartBooking.Core.Repositories.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DoctorsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorReadDto>>> GetAllDoctors()
        {
            var doctors = await _unitOfWork.Doctors.GetDoctorsWithDetailsAsync();
            var result = _mapper.Map<IEnumerable<DoctorReadDto>>(doctors);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DoctorReadDto>> GetDoctorById(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetDoctorWithDetailsByIdAsync(id);
            if (doctor == null)
                return NotFound("Doctor not found.");

            return Ok(_mapper.Map<DoctorReadDto>(doctor));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorCreateDto dto)
        {
            var doctor = _mapper.Map<Doctor>(dto);
            await _unitOfWork.Repository<Doctor>().AddAsync(doctor);
            await _unitOfWork.CompleteAsync();

            var createdDoctor = await _unitOfWork.Doctors.GetDoctorWithDetailsByIdAsync(doctor.Id);
            var result = _mapper.Map<DoctorReadDto>(createdDoctor);

            return CreatedAtAction(nameof(GetDoctorById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateDoctor(int id, [FromBody] DoctorUpdateDto dto)
        {
            var existingDoctor = await _unitOfWork.Repository<Doctor>().GetAsync(id);
            if (existingDoctor == null)
                return NotFound("Doctor not found for update.");

            _mapper.Map(dto, existingDoctor);

            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteDoctor(int id)
        {
            var doctor = await _unitOfWork.Repository<Doctor>().GetAsync(id);
            if (doctor == null)
                return NotFound("Doctor not found for deletion.");

            await _unitOfWork.Repository<Doctor>().DeleteAsync(doctor);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        [HttpGet("clinic/{clinicId:int}")]
        public async Task<ActionResult<IEnumerable<DoctorReadDto>>> GetDoctorsByClinic(int clinicId)
        {
            var doctors = await _unitOfWork.Doctors.GetDoctorsByClinicIdAsync(clinicId);
            var result = _mapper.Map<IEnumerable<DoctorReadDto>>(doctors);
            return Ok(result);
        }

        [HttpGet("speciality/{specialityId:int}")]
        public async Task<ActionResult<IEnumerable<DoctorReadDto>>> GetDoctorsBySpeciality(int specialityId)
        {
            var doctors = await _unitOfWork.Doctors.GetDoctorsBySpecialityIdAsync(specialityId);
            var result = _mapper.Map<IEnumerable<DoctorReadDto>>(doctors);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<DoctorReadDto>>> SearchDoctorsByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name is required for search.");

            var doctors = await _unitOfWork.Doctors.GetDoctorsByNameAsync(name);
            if (!doctors.Any())
                return NotFound("No doctors found matching the provided name.");

            var result = _mapper.Map<IEnumerable<DoctorReadDto>>(doctors);
            return Ok(result);
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<DoctorReadDto>>> GetDoctorsWithAvailableSlots()
        {
            var doctors = await _unitOfWork.Doctors.GetDoctorsWithAvailableSlotsAsync();
            var result = _mapper.Map<IEnumerable<DoctorReadDto>>(doctors);
            return Ok(result);
        }

        [HttpGet("exists")]
        public async Task<ActionResult<bool>> DoctorExistsByEmail([FromQuery] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest("Email is required.");

            bool exists = await _unitOfWork.Doctors.DoctorExistsByEmailAsync(email);
            return Ok(exists);
        }
    }
}
