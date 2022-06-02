using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces.CustomServices
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDTO>> Get();
        Task<AuthorDTO> GetAuthorById(string id);
        Task<IEnumerable<TableDTO>> GetAuthorTables(string id);
        Task Create(AuthorDTO author);
        Task Edit(AuthorDTO author);
        Task Delete(string id);
        Task<string> GetAuthorRoleAsync(Author author);
    }
}
