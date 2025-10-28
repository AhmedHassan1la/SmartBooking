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
    public class DoctorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

    public DoctorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

  

        private DoctorReadDto MapToReadDto(Doctor doctor)
        {
            if (doctor == null) return null;

            return new DoctorReadDto
            {
                Id = doctor.Id,
                AppUserId = doctor.AppUserId,
                Bio = doctor.Bio,
                Certifications = doctor.Certifications,
                Education = doctor.Education,
                YearOfExperience = doctor.YearOfExperience,
                ClinicName = doctor.Clinic?.Name,
                SpecialityName = doctor.Speciality?.Name,
                AppUserDisplayName = doctor.AppUser?.DisplayName
            };
        }

        private Doctor MapToEntity(DoctorCreateDto dto)
        {
            return new Doctor
            {
                AppUserId = dto.AppUserId,
                Bio = dto.Bio,
                Certifications = dto.Certifications,
                Education = dto.Education,
                YearOfExperience = dto.YearOfExperience,
                ClinicId = dto.ClinicId,
                SpecialityId = dto.SpecialityId
            };
        }

       

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorReadDto>>> GetAllDoctors()
        {
            var doctors = await _unitOfWork.Doctors.GetDoctorsWithDetailsAsync();
            var result = doctors.Select(MapToReadDto).ToList();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DoctorReadDto>> GetDoctorById(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetDoctorWithDetailsByIdAsync(id);
            if (doctor == null)
                return NotFound("Doctor not found.");

            return Ok(MapToReadDto(doctor));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorCreateDto dto)
        {
            var doctor = MapToEntity(dto);
            await _unitOfWork.Repository<Doctor>().AddAsync(doctor);
            await _unitOfWork.CompleteAsync();

            var createdDoctor = await _unitOfWork.Doctors.GetDoctorWithDetailsByIdAsync(doctor.Id);

            return CreatedAtAction(nameof(GetDoctorById),
                new { id = createdDoctor!.Id },
                MapToReadDto(createdDoctor));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateDoctor(int id, [FromBody] DoctorUpdateDto dto)
        {
            var existingDoctor = await _unitOfWork.Repository<Doctor>().GetAsync(id);
            if (existingDoctor == null)
                return NotFound("Doctor not found for update.");

            existingDoctor.Bio = dto.Bio;
            existingDoctor.Certifications = dto.Certifications;
            existingDoctor.Education = dto.Education;
            existingDoctor.YearOfExperience = dto.YearOfExperience;
            existingDoctor.ClinicId = dto.ClinicId;
            existingDoctor.SpecialityId = dto.SpecialityId;

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
            var result = doctors.Select(MapToReadDto).ToList();
            return Ok(result);
        }

        [HttpGet("speciality/{specialityId:int}")]
        public async Task<ActionResult<IEnumerable<DoctorReadDto>>> GetDoctorsBySpeciality(int specialityId)
        {
            var doctors = await _unitOfWork.Doctors.GetDoctorsBySpecialityIdAsync(specialityId);
            var result = doctors.Select(MapToReadDto).ToList();
            return Ok(result);
        }

        // GET: api/doctors/search?name=ahmed
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<DoctorReadDto>>> SearchDoctorsByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name is required for search.");

            var doctors = await _unitOfWork.Doctors.GetDoctorsByNameAsync(name);

            if (!doctors.Any())
                return NotFound("No doctors found matching the provided name.");

            var result = doctors.Select(MapToReadDto).ToList();
            return Ok(result);
        }

        // GET: api/doctors/available
        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<DoctorReadDto>>> GetDoctorsWithAvailableSlots()
        {
            var doctors = await _unitOfWork.Doctors.GetDoctorsWithAvailableSlotsAsync();
            var result = doctors.Select(MapToReadDto).ToList();
            return Ok(result);
        }

        // GET: api/doctors/exists?email=example@mail.com
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
