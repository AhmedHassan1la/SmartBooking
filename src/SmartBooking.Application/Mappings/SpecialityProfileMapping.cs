using AutoMapper;
using SmartBooking.Application.Dtos;
using SmartBooking.Core.Entities;

namespace SmartBooking.Application.Mappings
{
    public class SpecialityProfileMapping : Profile
    {
        public SpecialityProfileMapping()
        {
            // Entity → DTO
            CreateMap<Speciality, SpecialityReadDto>();

            // DTO → Entity
            CreateMap<SpecialityCreateDto, Speciality>();
            CreateMap<SpecialityUpdateDto, Speciality>();
        }
    }
}
