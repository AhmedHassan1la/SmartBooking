using SmartBooking.Core.Entities;
using SmartBooking.Core.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBooking.Core.Repositories.Interfaces
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {

        Task<Doctor?> GetDoctorById(string id);
        Task<IEnumerable<Doctor>> GetDoctorsWithDetailsAsync();

        Task<Doctor?> GetDoctorWithDetailsByIdAsync(string id);

        Task<IEnumerable<Doctor>> GetDoctorsByClinicIdAsync(int clinicId);

        Task<IEnumerable<Doctor>> GetDoctorsBySpecialityIdAsync(int specialityId);

        Task<IEnumerable<Doctor>> GetDoctorsByNameAsync(string name);

        Task<IEnumerable<Doctor>> GetDoctorsWithAvailableSlotsAsync();

        Task<bool> DoctorExistsByEmailAsync(string email);
    }
}
