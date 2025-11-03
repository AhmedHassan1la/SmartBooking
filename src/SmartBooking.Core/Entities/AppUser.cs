using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using SmartBooking.Core.Entities;

namespace SmartBooking.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Gender { get; set; } = "Not Specified";
        public string Address { get; set; } = "Not Provided";
        public string ImageUrl { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}