using SmartBooking.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBooking.Application.Services.Doctors;
public interface IDoctorService
{
    Task<IEnumerable<DoctorReadDto>> GetAllAsync();
    Task<IEnumerable<DoctorReadDto>> GetAllByClinicAsync(int clinicId);
    Task<IEnumerable<DoctorReadDto>> GetAllBySpecialityAsync(int specialityId);
    Task<IEnumerable<DoctorReadWithSlots>> GetAllWithSlots();
    Task<DoctorReadDto> GetByIdAsync(string id);
    Task<IEnumerable<DoctorReadDto>> SearchByName(string name);
    Task<DoctorReadDto> CreateAsync(DoctorCreateDto dto);
    Task<bool> UpdateAsync(string id, DoctorUpdateDto dto);
    Task<bool> DeleteAsync(string id);
}
