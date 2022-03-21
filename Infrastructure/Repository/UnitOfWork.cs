using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        // INITIAL DATABASE
        private ApplicationContext _context;
        // INITIAL REPOSITORIES
        private IRepository<Table> _tableRepository;
        private IRepository<Author> _authorRepository;
        public UnitOfWork(ApplicationContext context) { _context = context; } // CTOR
        // GET FOR REPOSITORY
        public IRepository<Table> TableRepository
        {
            get
            {
                if (_tableRepository == null)
                    _tableRepository = new Repository<Table>(_context);
                return _tableRepository;
            }
        }
        public IRepository<Author> AuthorRepository
        {
            get
            {
                if (_authorRepository == null)
                    _authorRepository = new Repository<Author>(_context);
                return _authorRepository;
            }
        }
        // REALISE Save();
        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
        // DISPOSING
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    _context.Dispose();
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
