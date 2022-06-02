using System.Threading.Tasks;

namespace Core.Interfaces.CustomServices
{
    public interface ICreateDirectory
    {
        Task CreateDirectoryAsync(string folderPath);
    }
}
