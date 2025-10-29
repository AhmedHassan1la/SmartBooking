using Microsoft.EntityFrameworkCore;
using SmartBooking.Core.Entities;
using SmartBooking.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBooking.Infrastructure.Data.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Doctor?> GetDoctorById(string id)
        {
            return await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
        }

        
        public async Task<IEnumerable<Doctor>> GetDoctorsWithDetailsAsync()
        {
            return await _context.Doctors
                .Include(d => d.Clinic)
                .Include(d => d.Speciality)
                .Include(d => d.AppointmentSlots)
                .AsNoTracking()
                .ToListAsync();
        }

        
        public async Task<Doctor?> GetDoctorWithDetailsByIdAsync(string id)
        {
            return await _context.Doctors
                .Include(d => d.Clinic)
                .Include(d => d.Speciality)
                .Include(d => d.AppointmentSlots)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        
        public async Task<IEnumerable<Doctor>> GetDoctorsByClinicIdAsync(int clinicId)
        {
            return await _context.Doctors
                .Include(d => d.Speciality)
                .Where(d => d.ClinicId == clinicId)
                .AsNoTracking()
                .ToListAsync();
        }

       
        public async Task<IEnumerable<Doctor>> GetDoctorsBySpecialityIdAsync(int specialityId)
        {
            return await _context.Doctors
                .Include(d => d.Clinic)
                .Where(d => d.SpecialityId == specialityId)
                .AsNoTracking()
                .ToListAsync();
        }

       
        public async Task<IEnumerable<Doctor>> GetDoctorsByNameAsync(string name)
        {
            return await _context.Doctors
                .Include(d => d.Clinic)
                .Include(d => d.Speciality)
                .Where(d => d.DisplayName.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }

       
        public async Task<IEnumerable<Doctor>> GetDoctorsWithAvailableSlotsAsync()
        {
            return await _context.Doctors
                .Include(d => d.Clinic)
                .Include(d => d.Speciality)
                .Include(d => d.AppointmentSlots)
                .Where(d => d.AppointmentSlots.Any(s =>
                    s.Date >= DateTime.UtcNow))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> DoctorExistsByEmailAsync(string email)
        {
            return await _context.Doctors
                .AnyAsync(d => d.Email.ToLower() == email.ToLower());
        }
    }
}
