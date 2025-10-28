using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBooking.Core.Entities
{
    public class Doctor : BaseEntity<int>
    {

        public string AppUserId { get; set; } = null;
        public virtual AppUser AppUser { get; set; }


        public string Bio { get; set; }
        public string Certifications { get; set; }
        public string Education { get; set; }
        public int YearOfExperience { get; set; }

        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }

        public int SpecialityId { get; set; }
        public Speciality Speciality { get; set; }

        public ICollection<AppointmentSlot> AppointmentSlots { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }

}
