using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SmartBooking.Core.Entities
{
    public class Clinic : BaseEntity
    {

        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<AppointmentSlot> AppointmentSlots { get; set; }
    }

}
