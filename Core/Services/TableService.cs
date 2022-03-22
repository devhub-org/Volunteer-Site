using AutoMapper;
using Core.DTO;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Interfaces.CustomServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class TableService : ITableService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TableService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // METHODS FOR SERVICES
        public async Task Create(TableDTO table)
        {
            if (table == null)
                throw new HttpException($"Error with create new table! Null!", HttpStatusCode.NotFound);
            await _unitOfWork.TableRepository.Insert(_mapper.Map<Table>(table));
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
        public async Task<IEnumerable<TableDTO>> Get()
        {
            return _mapper.Map<IEnumerable<TableDTO>>(await _unitOfWork.TableRepository.Get());
        }
        public async Task<TableDTO> GetTableById(int id)
        {
            if (id < 0) throw new HttpException($"Invalid id!", HttpStatusCode.BadGateway);
            var table = _unitOfWork.TableRepository.GetById(id);
            if (table == null) throw new HttpException($"Table Not Found! Null!", HttpStatusCode.NotFound);
            return _mapper.Map<TableDTO>(await table);
        }
    }
}
