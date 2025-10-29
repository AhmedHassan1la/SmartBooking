using AutoMapper;
using SmartBooking.Application.DTOs;
using SmartBooking.Core.Entities;

namespace SmartBooking.Application.Mappings
{
    public class ClinicProfileMapping : Profile
    {
        public ClinicProfileMapping()
        {
            // Entity → DTO
            CreateMap<Clinic, ClinicReadDto>();

            // DTO → Entity
            CreateMap<ClinicCreateDto, Clinic>();
            CreateMap<ClinicUpdateDto, Clinic>();
        }
    }
}
