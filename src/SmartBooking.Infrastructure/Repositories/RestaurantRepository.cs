using SmartBooking.Core.Entities;
using SmartBooking.Core.Repositories.Interfaces;
using SmartBooking.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBooking.Infrastructure.Repositories;
public class RestaurantRepository : GenericRepository<Restaurant>, IRestaurantRepository
{
    public RestaurantRepository(ApplicationDbContext context) : base(context)
    {
    }
}
