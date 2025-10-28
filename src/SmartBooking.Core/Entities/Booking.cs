using System;

namespace SmartBooking.Core.Entities
{
    public class Booking : BaseEntity<int>
    {
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }


        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int AppointmentSlotId { get; set; }
        public AppointmentSlot AppointmentSlot { get; set; }

        public DateTime BookedAt { get; set; } = DateTime.UtcNow;

        public string Status { get; set; }="Pending";   

        public decimal TotalAmount { get; set; }
    }
}
