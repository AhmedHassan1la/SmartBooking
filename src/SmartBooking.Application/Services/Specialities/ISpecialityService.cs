using SmartBooking.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBooking.Application.Services.Specialities;
public interface ISpecialityService
{
    Task<IEnumerable<SpecialityReadDto>> GetAllAsync();
    Task<SpecialityReadDto> GetByIdAsync(int id);
    Task<SpecialityReadDto> CreateAsync(SpecialityCreateDto specialityCreateDto);
    Task<bool> UpdateAsync(int id, SpecialityUpdateDto specialityUpdateDto);
    Task<bool> DeleteAsync(int id);
}
