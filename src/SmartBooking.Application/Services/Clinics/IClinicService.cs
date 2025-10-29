using SmartBooking.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBooking.Application.Services.Clinics;
public interface IClinicService
{
    Task<IEnumerable<ClinicReadDto>> GetAllAsync();
    Task<ClinicReadDto?> GetByIdAsync(int id);
    Task<ClinicReadDto> CreateAsync(ClinicCreateDto clinicCreateDto);
    Task<bool> UpdateAsync(int id, ClinicUpdateDto clinicUpdateDto);
    Task<bool> DeleteAsync(int id);
}
