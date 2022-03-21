using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            //services.AddScoped<IAuthorService, AuthorService>();
            //services.AddScoped<ITrackService, TrackService>();
        }
        public static void AddAutoMapper(this IServiceCollection services)
        {
            //var configures = new MapperConfiguration(mc =>
            //{
            //    mc.CreateMap<Table, TableDTO>().ReverseMap();
            //    mc.CreateMap<Author, AuthorDTO>().ReverseMap();
            //});

            //IMapper mapper = configures.CreateMapper();
            //services.AddSingleton(mapper);
        }
    }
}
