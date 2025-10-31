using AutoMapper;
using SmartBooking.Application.Dtos;
using SmartBooking.Core.Entities;
using SmartBooking.Core.Repositories.Interfaces;

namespace SmartBooking.Application.Services.Restaurants;
public class RestaurantService(IUnitOfWork unitOfWork , IMapper mapper) : IRestaurantService
{
    public async Task<RestaurantReadDto> CreateAsync(RestaurantDto dto)
    {
        var restaurant = mapper.Map<Restaurant>(dto);

        //TODO : Change image type to IFormFile and then upload image 

        await unitOfWork.Restaurants.AddAsync(restaurant);
        await unitOfWork.CompleteAsync();

        return mapper.Map<RestaurantReadDto>(restaurant);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var restaurant =await unitOfWork.Restaurants.GetAsync(id);

        if (restaurant is null)
            return false;

        await unitOfWork.Restaurants.DeleteAsync(restaurant);
        await unitOfWork.CompleteAsync();
        return true;
    }

    public async Task<IEnumerable<RestaurantReadDto>> GetAllAsync()
    {
        var restaurants = await unitOfWork.Restaurants.GetAllAsync();

        return mapper.Map<IEnumerable<RestaurantReadDto>>(restaurants);
    }

    public async Task<RestaurantReadDto?> GetByIdAsync(int id)
    {
        var restaurant = await unitOfWork.Restaurants.GetAsync(id);

        if (restaurant is null)
            return null;

        return mapper.Map<RestaurantReadDto>(restaurant);
    }

    public async Task<bool> UpdateAsync(int id, RestaurantDto dto)
    {
        var restaurant = await unitOfWork.Restaurants.GetAsync(id);

        if (restaurant is null)
            return false;

        mapper.Map(dto, restaurant);

        await unitOfWork.Restaurants.UpdateAsync(id,restaurant);

        await unitOfWork.CompleteAsync();
        return true;
    }
}
