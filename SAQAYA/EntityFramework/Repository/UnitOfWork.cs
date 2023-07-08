using Microsoft.EntityFrameworkCore;

namespace SAQAYA.EntityFramework.Repository
{

    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext, new()
    {
        private readonly TContext _context;

        private bool disposed;

        public bool isDisposed { get { return disposed; } }


        public UnitOfWork(TContext context)
        {
            _context = context;
            disposed = false;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            Repository<T> repo = new Repository<T>(_context);
            return repo;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        //public void BulkSaveChanges()
        //{
        //    _context.BulkSaveChanges();
        //}
        //// Bulk Save give a beter preforance when we save large the data
        //public async Task BulkSaveChangesAsync()
        //{
        //    await _context.BulkSaveChangesAsync();
        //}
        // Bulk Save give a beter preforance when we save large the data
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
