using AutoMapper;
using SmartBooking.Application.DTOs;
using SmartBooking.Core.Entities;
using SmartBooking.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBooking.Application.Services.Clinics;
public class ClinicService(IUnitOfWork unitOfWork,IMapper mapper) : IClinicService
{
    public async Task<ClinicReadDto> CreateAsync(ClinicCreateDto dto)
    {
        var clinic = mapper.Map<Clinic>(dto);

        await unitOfWork.Clinics.AddAsync(clinic);
        await unitOfWork.CompleteAsync();

        return mapper.Map<ClinicReadDto>(clinic);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var clinic = await unitOfWork.Clinics.GetAsync(id);

        if (clinic == null)
            return false;

        await unitOfWork.Clinics.DeleteAsync(clinic);
        await unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<IEnumerable<ClinicReadDto>> GetAllAsync()
    {
        var clinics = await unitOfWork.Clinics.GetAllAsync();
        return mapper.Map<IEnumerable<ClinicReadDto>>(clinics);
    }

    public async Task<ClinicReadDto?> GetByIdAsync(int id)
    {
        var clinic = await unitOfWork.Clinics.GetAsync(id);
        if (clinic == null) return null;

        return mapper.Map<ClinicReadDto>(clinic);
    }

    public async Task<bool> UpdateAsync(int id, ClinicUpdateDto dto)
    {
        var existingClinic = await unitOfWork.Clinics.GetAsync(id);

        if (existingClinic == null)
            return false;

        mapper.Map(dto, existingClinic);

        await unitOfWork.Clinics.UpdateAsync(id, existingClinic);
        await unitOfWork.CompleteAsync();

        return true;
    }
}
