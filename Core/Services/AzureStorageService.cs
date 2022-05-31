﻿using Core.Interfaces.CustomServices;
using System;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using System.IO;
using Microsoft.Extensions.Options;
using Core.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.StaticFiles;
using Core.Exeptions.FileExceptions;
using Azure.Storage.Blobs.Models;
using Core.ApiModels;

namespace Core.Services
{
    public class AzureBlobStorageService : IAzureBlobStorageService
    {
        private readonly IOptions<FileSettings> _fileSettings;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IMapper _mapper;

        public AzureBlobStorageService(IOptions<FileSettings> fileSettings,
            BlobServiceClient blobServiceClient,
            IMapper mapper)
        {
            _fileSettings = fileSettings;
            _blobServiceClient = blobServiceClient;
            _mapper = mapper;
        }

        public async Task<string> AddFileAsync(Stream stream, string folderPath, string fileName)
        {
            if (stream == null)
            {
                throw new FileIsEmptyException(fileName);
            }

            await CreateDirectoryAsync(folderPath);

            string uniqueFileName = CreateName(fileName, folderPath);

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(fileName, out string contentType))
            {
                throw new CannotGetFileContentTypeException(fileName);
            }

            var blob = _blobServiceClient.GetBlobContainerClient(folderPath)
                .GetBlobClient(uniqueFileName);

            BlobHttpHeaders blobHttpHeaders = new BlobHttpHeaders();
            blobHttpHeaders.ContentType = contentType;

            await blob.UploadAsync(stream, blobHttpHeaders);

            return StorageTypes.AzureBlob.ToString() + ":" + Path.Combine(folderPath, uniqueFileName);
        }

        public async Task CreateDirectoryAsync(string folderPath)
        {
            if (!_blobServiceClient.GetBlobContainerClient(folderPath).Exists())
            {
                if (!_fileSettings.Value.AllowCreateFolderPath)
                {
                    throw new FileFolderNotExistException(folderPath);
                }
                else
                {
                    await _blobServiceClient.CreateBlobContainerAsync(folderPath);
                }
            }
        }

        public async Task DeleteFileAsync(string path)
        {
            var folderPath = Path.GetDirectoryName(path);
            var fileName = Path.GetFileName(path);

            var blob = _blobServiceClient.GetBlobContainerClient(folderPath)
                .GetBlobClient(fileName);

            await blob.DeleteIfExistsAsync();
        }

        public async Task<DownloadFile> GetFileAsync(string path)
        {
            var folderPath = Path.GetDirectoryName(path);
            var fileName = Path.GetFileName(path);

            var blob = _blobServiceClient.GetBlobContainerClient(folderPath)
                .GetBlobClient(fileName);

            BlobDownloadInfo download = await blob.DownloadAsync();

            var fileToReturn = _mapper.Map<DownloadFile>(download);
            fileToReturn.Name = fileName;

            return fileToReturn;
        }

        private string CreateName(string fileName, string folderPath)
        {
            var blob = _blobServiceClient.GetBlobContainerClient(folderPath).GetBlobClient(fileName);

            if (blob.Exists())
            {
                if (!_fileSettings.Value.AllowChangeName)
                {
                    throw new FileNameAlreadyExistException(Path.Combine(folderPath, fileName));
                }
                else
                {
                    return $"{Path.GetFileNameWithoutExtension(fileName)}_" +
                        $"{DateTime.Now.ToString("yyyyMMddTHHmmssfff")}" +
                        $"{Path.GetExtension(fileName)}";
                }
            }

            return fileName;
        }
    }

}
