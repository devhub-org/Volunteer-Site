using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO.Table;
using Core.Entities;
using Core.DTO.Author;

namespace Core.Interfaces.CustomServices
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorResponseDTO>> Get();
        Task<AuthorResponseDTO> GetAuthorById(string id);
        Task<IEnumerable<TableResponseDTO>> GetAuthorTables(string id);
        Task Create(AuthorDTO author);
        Task Edit(AuthorDTO author);
        Task Delete(string id);
        Task<string> GetAuthorRoleAsync(Author author);
    }
}
