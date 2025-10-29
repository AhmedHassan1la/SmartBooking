using Microsoft.AspNetCore.Mvc;
using SmartBooking.Application.Dtos;
using SmartBooking.Application.Services.Doctors;

namespace SmartBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController(IDoctorService doctorService) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorReadDto>>> GetAllDoctors() =>
            Ok(await doctorService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorReadDto>> GetDoctorById([FromRoute] string id)
        {
            var doctor = await doctorService.GetByIdAsync(id);

            return doctor is null ? NotFound("Doctor not found") : Ok(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorCreateDto dto)
        {
            var doctor = await doctorService.CreateAsync(dto);
            
            return CreatedAtAction(nameof(GetDoctorById), new { id = doctor.Id }, doctor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDoctor([FromRoute] string id, [FromBody] DoctorUpdateDto dto)
        {
            var isUpdated = await doctorService.UpdateAsync(id, dto);

            return !isUpdated ? NotFound("Doctor not found.") : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDoctor([FromRoute] string id)
        {
            var isDeleted = await doctorService.DeleteAsync(id);

            return !isDeleted ? NotFound("Doctor not found.") : NoContent();

        }

        [HttpGet("clinic/{clinicId}")]
        public async Task<ActionResult<IEnumerable<DoctorReadDto>>> GetDoctorsByClinic([FromRoute] int clinicId)
        {
            return Ok(await doctorService.GetAllByClinicAsync(clinicId));
        }

        [HttpGet("speciality/{specialityId}")]
        public async Task<ActionResult<IEnumerable<DoctorReadDto>>> GetDoctorsBySpeciality([FromRoute] int specialityId)
        {
            return Ok(await doctorService.GetAllBySpecialityAsync(specialityId));
        }

        [HttpGet("search/{name}")]
        public async Task<ActionResult<IEnumerable<DoctorReadDto>>> SearchDoctorsByName([FromRoute] string name)
        {
            return Ok(await doctorService.SearchByName(name));
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<DoctorReadWithSlots>>> GetDoctorsWithAvailableSlots()
        {
            return Ok(await doctorService.GetAllWithSlots());
        }

    }
}
