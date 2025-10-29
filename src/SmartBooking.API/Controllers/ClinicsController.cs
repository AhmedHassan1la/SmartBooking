using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartBooking.Application.DTOs;
using SmartBooking.Application.Services.Clinics;
using SmartBooking.Core.Entities;
using SmartBooking.Core.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicsController(IClinicService clinicService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClinicReadDto>>> GetAllClinics() =>
            Ok(await clinicService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<ClinicReadDto>> GetClinicById([FromRoute] int id)
        {
            var clinic = await clinicService.GetByIdAsync(id);

            return clinic is null ? NotFound("Clinic not found") : Ok(clinic);
        }

        [HttpPost]
        public async Task<ActionResult<ClinicReadDto>> CreateClinic([FromBody] ClinicCreateDto dto)
        {
            var clinic =await clinicService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetClinicById), new { id = clinic.Id }, clinic);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClinic([FromRoute]int id, [FromBody] ClinicUpdateDto dto)
        {
            var isUpdated = await clinicService.UpdateAsync(id,dto);

            return !isUpdated ? NotFound("Clinic not found") : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClinic(int id)
        {
            var isDeleted = await clinicService.DeleteAsync(id);
            
            return !isDeleted ? NotFound("Clinic not found") : NoContent();
        }
    }
}
