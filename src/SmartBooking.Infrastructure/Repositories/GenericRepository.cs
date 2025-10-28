using Microsoft.EntityFrameworkCore;
using SmartBooking.Core.Repositories.Interfaces;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SmartBooking.Infrastructure.Data
{
    // نفترض أنك تستخدم اسم مشروعك هنا بدلاً من 'SmartBookingDbContext'
    // يجب أن تكون الفئة 'TContext' هي الـ DbContext الخاص بك.
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // -------------------------------------------------------------------
        // ## العمليات الأساسية (CRUD)
        // -------------------------------------------------------------------

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            // ملاحظة: حفظ التغييرات (SaveChanges) يتم عادةً في وحدة العمل (UnitOfWork)
            // لكن يمكنك إضافتها هنا إذا كنت لا تستخدم UnitOfWork.
            // await _context.SaveChangesAsync();
        }
        public Task UpdateAsync(int id, T entity)
        {
            var existing = _dbSet.Find(id);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(entity);
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }


        // -------------------------------------------------------------------
        // ## الحصول على الكل (Get All)
        // -------------------------------------------------------------------

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        // -------------------------------------------------------------------
        // ## الحصول على الكل مع التضمين (Includes)
        // -------------------------------------------------------------------

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>>[] Includes)
        {
            IQueryable<T> query = _dbSet;

            if (Includes != null)
            {
                foreach (var include in Includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.ToListAsync();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, object>>[] Includes)
        {
            IQueryable<T> query = _dbSet;

            if (Includes != null)
            {
                foreach (var include in Includes)
                {
                    query = query.Include(include);
                }
            }
            // Execute the query and return the result
            return query.ToList();
        }

        // -------------------------------------------------------------------
        // ## الحصول بالمعرّف (Get By Id)
        // -------------------------------------------------------------------

        public async Task<T> GetAsync(int id)
        {
            // FindAsync هي الطريقة الأسرع للبحث بواسطة المفتاح الرئيسي
            return await _dbSet.FindAsync(id);
        }
    }
}