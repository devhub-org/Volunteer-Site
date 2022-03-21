using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Table> TableRepository { get; }
        IRepository<Author> AuthorRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
