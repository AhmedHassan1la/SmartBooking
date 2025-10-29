using AutoMapper;
using SmartBooking.Application.Dtos;
using SmartBooking.Core.Entities;

namespace SmartBooking.Application.Mapping
{
    public class DoctorProfileMapping : Profile
    {
        public DoctorProfileMapping()
        {
            // Doctor → DoctorReadDto
            CreateMap<Doctor, DoctorReadDto>()
                .ForMember(dest => dest.ClinicName, opt => opt.MapFrom(src => src.Clinic.Name))
                .ForMember(dest => dest.SpecialityName, opt => opt.MapFrom(src => src.Speciality.Name))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.DisplayName));

            // DoctorCreateDto → Doctor
            CreateMap<DoctorCreateDto, Doctor>();

            // DoctorUpdateDto → Doctor
            CreateMap<DoctorUpdateDto, Doctor>();


            CreateMap<Doctor, DoctorReadWithSlots>()
                .ForMember(dest => dest.ClinicName, opt => opt.MapFrom(src => src.Clinic.Name))
                .ForMember(dest => dest.SpecialityName, opt => opt.MapFrom(src => src.Speciality.Name))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.Slots, opt => opt.MapFrom(src => src.AppointmentSlots));

            CreateMap<AppointmentSlot, DoctorSlotDto>();
        }
    }
}
