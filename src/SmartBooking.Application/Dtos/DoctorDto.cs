using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBooking.Application.Dtos
{
    public class DoctorCreateDto
    {
        public string AppUserId { get; set; } = null;
        public string Bio { get; set; }
        public string Certifications { get; set; }
        public string Education { get; set; }
        public int YearOfExperience { get; set; }
        public int ClinicId { get; set; }
        public int SpecialityId { get; set; }
    }

    public class DoctorUpdateDto
    {
        
        public string Bio { get; set; }
        public string Certifications { get; set; }
        public string Education { get; set; }
        public int YearOfExperience { get; set; }
        public int ClinicId { get; set; }
        public int SpecialityId { get; set; }
    }

    public class DoctorReadDto
    {
        public int Id { get; set; } 
        public string AppUserId { get; set; }
        public string Bio { get; set; }
        public string Certifications { get; set; }
        public string Education { get; set; }
        public int YearOfExperience { get; set; }

        public string ClinicName { get; set; } 
        public string SpecialityName { get; set; } 

        public string AppUserDisplayName { get; set; }
    }
}