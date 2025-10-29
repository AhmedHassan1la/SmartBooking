using System;

namespace SmartBooking.Core.Entities
{
    public class Booking : BaseEntity
    {
        public string UserId { get; set; }
        public virtual AppUser User { get; set; }


        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int AppointmentSlotId { get; set; }
        public AppointmentSlot AppointmentSlot { get; set; }

        public DateTime BookedAt { get; set; } = DateTime.UtcNow;

        public string Status { get; set; }="Pending";   

        public decimal TotalAmount { get; set; }
    }
}
