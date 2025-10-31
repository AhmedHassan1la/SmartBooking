using SmartBooking.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBooking.Application.Services.Restaurants;
public interface IRestaurantService
{
    Task<IEnumerable<RestaurantReadDto>> GetAllAsync();
    Task<RestaurantReadDto> GetByIdAsync(int id);
    Task<RestaurantReadDto> CreateAsync(RestaurantDto dto);
    Task<bool> UpdateAsync(int id, RestaurantDto dto);
    Task<bool> DeleteAsync(int id);
}
