using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBooking.Core.Entities
{
    public class Restaurant : BaseEntity<int>
    {

        public string Name { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; }
        public ICollection<Order> Orders { get; set; }
    }

}
