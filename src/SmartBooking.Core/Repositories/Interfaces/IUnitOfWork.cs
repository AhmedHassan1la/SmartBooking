using System;
using System.Threading.Tasks;

namespace SmartBooking.Core.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDoctorRepository Doctors { get; }
        IClinicRepository Clinics { get; }
        ISpecialityRepository Specialities { get; }
        IRestaurantRepository Restaurants { get; }
    
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;

        Task<int> CompleteAsync();
    }
}
