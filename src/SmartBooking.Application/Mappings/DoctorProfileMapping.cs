using AutoMapper;
using SmartBooking.Application.Dtos;
using SmartBooking.Core.Entities;

namespace SmartBooking.Application.Mapping
{
    public class DoctorProfileMapping : Profile
    {
        public DoctorProfileMapping()
        {
            // من Doctor → DoctorReadDto
            CreateMap<Doctor, DoctorReadDto>()
                .ForMember(dest => dest.ClinicName, opt => opt.MapFrom(src => src.Clinic.Name))
                .ForMember(dest => dest.SpecialityName, opt => opt.MapFrom(src => src.Speciality.Name))
                .ForMember(dest => dest.AppUserDisplayName, opt => opt.MapFrom(src => src.AppUser.DisplayName));

            // من DoctorCreateDto → Doctor
            CreateMap<DoctorCreateDto, Doctor>();

            // من DoctorUpdateDto → Doctor
            CreateMap<DoctorUpdateDto, Doctor>();
        }
    }
}
