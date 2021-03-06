using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomServices
{
    public interface IFileStorageService
    {
        Task DeleteFile(string containerName, string fileRoute);
        Task<string> UploadFile(string containerName, IFormFile file);
        Task<string> EditFile(string containerName, string oldFileRoute, IFormFile newFile);
    }
}
