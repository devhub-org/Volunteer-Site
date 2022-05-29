using AutoMapper;
using Core.DTO;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.CustomServices;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IFileStorageService, AzureStorageService>();
        }
        public static void AddAutoMapper(this IServiceCollection services)
        {
            var configures = new MapperConfiguration(mc =>
            {
                mc.CreateMap<Author, AuthorDTO>().ReverseMap();
                mc.CreateMap<Table, TableDTO>().ReverseMap();
            });

            IMapper mapper = configures.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
