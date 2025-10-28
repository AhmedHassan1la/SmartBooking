using SmartBooking.Core.Entities;
using SmartBooking.Core.Repositories.Interfaces;
using SmartBooking.Infrastructure.Data;

namespace SmartBooking.Infrastructure.Repositories
{
    public class SpecialityRepository : GenericRepository<Speciality>, ISpecialityRepository
    {
        public SpecialityRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
