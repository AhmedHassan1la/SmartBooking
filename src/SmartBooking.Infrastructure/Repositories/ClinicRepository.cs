using SmartBooking.Core.Entities;
using SmartBooking.Core.Repositories.Interfaces;
using SmartBooking.Infrastructure.Data;

namespace SmartBooking.Infrastructure.Repositories
{
    public class ClinicRepository : GenericRepository<Clinic>, IClinicRepository
    {
        public ClinicRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
