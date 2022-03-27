using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
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

            public static void AddIdentity(this IServiceCollection services)
            {
                services.AddIdentity<Author, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationContext>()
                    .AddDefaultTokenProviders();
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
