using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomServices
{
    public interface ITableService
    {
        Task<IEnumerable<TableDTO>> Get();
        Task<TableDTO> GetTableById(int id);
        Task Create(TableDTO author);
        Task Edit(TableDTO author);
        Task Delete(int id);
    }
}
