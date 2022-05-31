using Core.ApiModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomServices
{
    public interface IFileService
    {
        Task<string> AddFileAsync(Stream stream, string folderPath, string fileName);
        Task<DownloadFile> GetFileAsync(string path);
        Task DeleteFileAsync(string path);
    }
}
