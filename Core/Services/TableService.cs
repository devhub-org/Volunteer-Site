using AutoMapper;
using Core.DTO;
using Core.Entities;
using Core.Exceptions;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.CustomServices;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Core.DTO.Table;

namespace Core.Services
{
    public class TableService : ITableService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IOptions<ImageSettings> _imageSettings;
        public TableService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IFileService fileService,
            IOptions<ImageSettings> imageSettings)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
            _imageSettings = imageSettings;
        }
        // METHODS FOR SERVICES
        public async Task Create(TableDTO table)
        {
            if (table == null)
                throw new HttpException($"Error with create new table! Null!", HttpStatusCode.NotFound);

            string newPath = await _fileService.AddFileAsync(table.Image.OpenReadStream(), 
                _imageSettings.Value.Path, table.Image.FileName);

            var newTable = _mapper.Map<Table>(table);
            newTable.Image = newPath;
            await _unitOfWork.TableRepository.Insert(newTable);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            if (id < 0) throw new HttpException($"Invalid id!", HttpStatusCode.NotFound);
            var table = _unitOfWork.TableRepository.GetById(id);
            if (table != null)
                await _unitOfWork.TableRepository.Delete(table);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task Edit(TableDTO table)
        {
            if (table == null)
                throw new HttpException($"Error with edit table! Null!", HttpStatusCode.NotFound);
            _unitOfWork.TableRepository.Update(_mapper.Map<Table>(table));
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<IEnumerable<TableResponseDTO>> Get()
        {
            return _mapper.Map<IEnumerable<TableResponseDTO>>(await _unitOfWork.TableRepository.Get());
        }
        public async Task<TableResponseDTO> GetTableById(int id)
        {
            if (id < 0) throw new HttpException($"Invalid id!", HttpStatusCode.BadGateway);
            var table = _unitOfWork.TableRepository.GetById(id);
            if (table == null) throw new HttpException($"Table Not Found! Null!", HttpStatusCode.NotFound);
            return _mapper.Map<TableResponseDTO>(await table);
        }
    }
}
