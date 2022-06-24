using AutoMapper;
using Core.DTO;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Interfaces.CustomServices;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Core.DTO.Table;
using Microsoft.AspNetCore.Identity;
using Core.Helpers;
using Microsoft.Extensions.Options;
using Core.DTO.Author;

namespace Core.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<Author> _userManager;
        private readonly IFileService _fileService;
        private readonly IOptions<ImageSettings> _imageSettings;
        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<Author> userManager, IFileService fileService,
            IOptions<ImageSettings> imageSettings)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _fileService = fileService;
            _imageSettings = imageSettings;
        }
        // METHODS FOR SERVICES
        public async Task Create(AuthorDTO author)
        {
            if (author == null)
                throw new HttpException($"Error with create new author!", HttpStatusCode.NotFound);

            string newPath = await _fileService.AddFileAsync(author.Avatar.OpenReadStream(),
                _imageSettings.Value.Path, author.Avatar.FileName);

            var newAuthor = _mapper.Map<Author>(author);
            newAuthor.Avatar = newPath;
            await _unitOfWork.AuthorRepository.Insert(newAuthor);
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

        public async Task<IEnumerable<AuthorResponseDTO>> Get()
        {
            return _mapper.Map<IEnumerable<AuthorResponseDTO>>(await _unitOfWork.AuthorRepository.Get());
        }

        public async Task<AuthorResponseDTO> GetAuthorById(string id)
        {
            if (id == null) throw new HttpException($"Invalid id!", HttpStatusCode.BadGateway);
            var author = _unitOfWork.AuthorRepository.GetById(id);
            if (author == null) throw new HttpException($"Author Not Found!", HttpStatusCode.NotFound);
            return _mapper.Map<AuthorResponseDTO>(await author);
        }

        public async Task<IEnumerable<TableResponseDTO>> GetAuthorTables(string id)
        {
            if (id == null) throw new HttpException($"Invalid id {id}!", HttpStatusCode.BadGateway);
            var tables = _mapper.Map<IEnumerable<TableResponseDTO>>(await _unitOfWork.TableRepository.Get(e => e.AuthorId == id));
            return tables;
        }
    }
}
