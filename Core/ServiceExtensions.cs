using AutoMapper;
using Azure.Storage.Blobs;
using Core.DTO;
using Core.DTO.Author;
using Core.DTO.Table;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces.CustomServices;
using Core.Services;
using Microsoft.Extensions.Configuration;
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
            services.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>(); // maybe need remove
        }

        public static void AddFileService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(_ =>
                new BlobServiceClient(configuration.GetSection("AzureBlobStorageSettings")
                    .GetValue<string>("AccessKey")));

            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>();
            services.AddScoped<ILocaleStorageService, LocaleStorageService>();
        }

        public static void ConfigureImageSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ImageSettings>(configuration.GetSection("ImageSettings"));
        }

        public static void ConfigureFileSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FileSettings>(configuration.GetSection("FileSettings"));
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            var configures = new MapperConfiguration(mc =>
            {
                mc.CreateMap<Author, AuthorDTO>().ReverseMap();
                mc.CreateMap<RegisterUserDTO, Author>().ReverseMap()
                .ForMember(dest => dest.Avatar,
                                       opt =>
                                           opt.MapFrom(src =>
                                               Constants.BlobPath + Constants.BlobPathImages + src.Avatar.Substring(17)));
                mc.CreateMap<Author, AuthorResponseDTO>()
                                   .ForMember(dest => dest.Avatar,
                                       opt =>
                                           opt.MapFrom(src =>
                                               Constants.BlobPath + Constants.BlobPathImages + src.Avatar.Substring(17)));
                mc.CreateMap<Table, TableDTO>().ReverseMap();
                mc.CreateMap<Table, TableResponseDTO>()
                    .ForMember(dest => dest.Image,
                        opt =>
                            opt.MapFrom(src =>
                                Constants.BlobPath + Constants.BlobPathImages + src.Image.Substring(17)));
            });

            IMapper mapper = configures.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
