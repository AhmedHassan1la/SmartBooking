using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBooking.Core.Entities
{
    public class Order : BaseEntity<int>
    {

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending";

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
