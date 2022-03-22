using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
        public static class ServiceExtensions
        {
            public static void AddDbContext(this IServiceCollection services,
                string connectionString)
            {
                services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString));
            }
            public static void AddRepository(this IServiceCollection services)
            {
                services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            }
            public static void AddUnitOfWork(this IServiceCollection services)
            {
                services.AddTransient<IUnitOfWork, UnitOfWork>();
            }
        }
}
