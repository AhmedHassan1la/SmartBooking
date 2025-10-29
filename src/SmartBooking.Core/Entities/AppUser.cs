using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using SmartBooking.Core.Entities;

namespace SmartBooking.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}