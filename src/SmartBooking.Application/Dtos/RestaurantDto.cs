using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBooking.Application.Dtos;
public class RestaurantDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public string Phone { get; set; }
}
