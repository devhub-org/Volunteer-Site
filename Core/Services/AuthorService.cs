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
using Core.DTO.Table;
using Microsoft.AspNetCore.Identity;

namespace Core.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<Author> _userManager;
        public AuthorService(IUnitOfWork unitOfWork,IMapper mapper, UserManager<Author> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }
        // METHODS FOR SERVICES
        public async Task Create(AuthorDTO author)
        {
            if (author == null)
                throw new HttpException($"Error with create new author!", HttpStatusCode.NotFound);
            await _unitOfWork.AuthorRepository.Insert(_mapper.Map<Author>(author));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            if (id == null) throw new HttpException($"Invalid id!", HttpStatusCode.NotFound);
            var author = _unitOfWork.AuthorRepository.GetById(id);
            if (author != null)
                await _unitOfWork.AuthorRepository.Delete(author);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<string> GetAuthorRoleAsync(Author author)
        {
            var userRoles = await _userManager.GetRolesAsync(author);
            if (userRoles == null || userRoles.Count == 0) throw new HttpException("User haven`t roles", HttpStatusCode.BadRequest);
            return userRoles.First();
        }

        public async Task Edit(AuthorDTO author)
        {
            if (author == null)
                throw new HttpException($"Error with edit author!", HttpStatusCode.NotFound);
            _unitOfWork.AuthorRepository.Update(_mapper.Map<Author>(author));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<AuthorDTO>> Get()
        {
            return _mapper.Map<IEnumerable<AuthorDTO>>(await _unitOfWork.AuthorRepository.Get());
        }

        public async Task<AuthorDTO> GetAuthorById(string id)
        {
            if (id == null) throw new HttpException($"Invalid id!", HttpStatusCode.BadGateway);
            var author = _unitOfWork.AuthorRepository.GetById(id);
            if (author == null) throw new HttpException($"Author Not Found!", HttpStatusCode.NotFound);
            return _mapper.Map<AuthorDTO>(await author);
        }

        public async Task<IEnumerable<TableResponseDTO>> GetAuthorTables(string id)
        {
            if (id == null) throw new HttpException($"Invalid id {id}!", HttpStatusCode.BadGateway);
            var tables = _mapper.Map<IEnumerable<TableResponseDTO>>(await _unitOfWork.TableRepository.Get(e => e.AuthorId == id));
            return tables;
        }
    }
}
