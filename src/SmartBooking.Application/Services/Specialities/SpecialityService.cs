using AutoMapper;
using SmartBooking.Application.Dtos;
using SmartBooking.Core.Entities;
using SmartBooking.Core.Repositories.Interfaces;

namespace SmartBooking.Application.Services.Specialities;
public class SpecialityService(IUnitOfWork unitOfWork, IMapper mapper) : ISpecialityService
{
    public async Task<SpecialityReadDto> CreateAsync(SpecialityCreateDto dto)
    {
        var speciality = mapper.Map<Speciality>(dto);

        await unitOfWork.Specialities.AddAsync(speciality);
        await unitOfWork.CompleteAsync();

        return mapper.Map<SpecialityReadDto>(speciality);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var speciality = await unitOfWork.Specialities.GetAsync(id);
        if (speciality == null)
            return false;

        await unitOfWork.Specialities.DeleteAsync(speciality);
        await unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<IEnumerable<SpecialityReadDto>> GetAllAsync()
    {
        var specialities = await unitOfWork.Specialities.GetAllAsync();
        return mapper.Map<IEnumerable<SpecialityReadDto>>(specialities);
    }

    public async Task<SpecialityReadDto> GetByIdAsync(int id)
    {
        var speciality = await unitOfWork.Specialities.GetAsync(id);
        if (speciality == null)
            return null;

        return mapper.Map<SpecialityReadDto>(speciality);
    }

    public async Task<bool> UpdateAsync(int id, SpecialityUpdateDto dto)
    {
        var existing = await unitOfWork.Specialities.GetAsync(id);
        if (existing == null)
            return false;

        mapper.Map(dto, existing);

        await unitOfWork.CompleteAsync();

        return true;
    }
}
