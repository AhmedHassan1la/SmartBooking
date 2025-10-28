using Microsoft.Extensions.DependencyInjection;
using SmartBooking.Core.Repositories.Interfaces;
using SmartBooking.Infrastructure.Data;
using SmartBooking.Infrastructure.Repositories; // لاستخدام GenericRepository<T>


// يجب وضع هذا الملف (InfrastructureRegistration.cs) داخل مجلد في مشروع SmartBooking.Infrastructure
namespace SmartBooking.Infrastructure
{
    public static class InfrastructureRegistration
    {
        // دالة إضافية لتسجيل خدمات البنية التحتية
        public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services)
        {
            // تسجيل خدمة الـ Generic Repository
            // يتم استخدام AddScoped لأن الـ DbContext (الذي يتم حقنه في Repository)
            // هو عادةً Scoped أيضًا، ويجب أن يكون لهما نفس العمر الافتراضي.

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork,UnitOfWork>();

            return services;
        }
    }
}