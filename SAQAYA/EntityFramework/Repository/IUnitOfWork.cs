namespace SAQAYA.EntityFramework.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class;

        void SaveChanges();

        Task SaveChangesAsync();

    }
}
