using AutoMapper;
using SmartBooking.Application.Dtos;
using SmartBooking.Core.Entities;
using SmartBooking.Core.Repositories.Interfaces;

namespace SmartBooking.Application.Services.Doctors;

public class DoctorService(IUnitOfWork unitOfWork, IMapper mapper) : IDoctorService
{

    public async Task<IEnumerable<DoctorReadDto>> GetAllAsync()
    {
        var doctors = await unitOfWork.Doctors.GetDoctorsWithDetailsAsync();
        return mapper.Map<IEnumerable<DoctorReadDto>>(doctors);
    }

    public async Task<DoctorReadDto> GetByIdAsync(string id)
    {
        var doctor = await unitOfWork.Doctors.GetDoctorWithDetailsByIdAsync(id);

        if (doctor == null)
            return null;

        return mapper.Map<DoctorReadDto>(doctor);
    }

    public async Task<DoctorReadDto> CreateAsync(DoctorCreateDto dto)
    {
        //TODO: Add user with email and password and assign doctor to this user

        var doctor = mapper.Map<Doctor>(dto);
        await unitOfWork.Doctors.AddAsync(doctor);
        await unitOfWork.CompleteAsync();

        return mapper.Map<DoctorReadDto>(doctor);
    }

    public async Task<bool> UpdateAsync(string id, DoctorUpdateDto dto)
    {
        var existingDoctor = await unitOfWork.Doctors.GetDoctorById(id);

        if (existingDoctor == null)
            return false;

        mapper.Map(dto, existingDoctor);

        await unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var doctor = await unitOfWork.Doctors.GetDoctorById(id);

        if (doctor == null)
            return false;

        await unitOfWork.Doctors.DeleteAsync(doctor);
        await unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<IEnumerable<DoctorReadDto>> GetAllByClinicAsync(int clinicId)
    {
        var doctors = await unitOfWork.Doctors.GetDoctorsByClinicIdAsync(clinicId);

        return mapper.Map<IEnumerable<DoctorReadDto>>(doctors);
    }

    public async Task<IEnumerable<DoctorReadDto>> GetAllBySpecialityAsync(int specialityId)
    {
        var doctors = await unitOfWork.Doctors.GetDoctorsBySpecialityIdAsync(specialityId);

        return mapper.Map<IEnumerable<DoctorReadDto>>(doctors);
    }

    public async Task<IEnumerable<DoctorReadDto>> SearchByName(string name)
    {
        var doctors = await unitOfWork.Doctors.GetDoctorsByNameAsync(name);

        return mapper.Map<IEnumerable<DoctorReadDto>>(doctors);
    }

    public async Task<IEnumerable<DoctorReadWithSlots>> GetAllWithSlots()
    {
        var doctors = await unitOfWork.Doctors.GetDoctorsWithAvailableSlotsAsync();

        return mapper.Map<IEnumerable<DoctorReadWithSlots>>(doctors);
    }

}
