using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBooking.Core.Entities
{
    public class AppointmentSlot:BaseEntity<int>
    {

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }

}
