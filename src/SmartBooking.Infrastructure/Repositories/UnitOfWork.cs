using Microsoft.EntityFrameworkCore;
using SmartBooking.Core.Repositories.Interfaces;
using SmartBooking.Infrastructure.Data;
using SmartBooking.Infrastructure.Data.Repositories;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace SmartBooking.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private Hashtable _repositories;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        // Repositories
        public IDoctorRepository Doctors => new DoctorRepository(_context);
        public IClinicRepository Clinics => new ClinicRepository(_context);
        public ISpecialityRepository Specialities => new SpecialityRepository(_context);
     
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            _repositories ??= new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryInstance = new GenericRepository<TEntity>(_context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)_repositories[type];
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
