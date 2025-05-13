using MainTest.Framework.Entity;

namespace MainTest.Framework.DataAccess;

public interface IUnitOfWork : IDisposable
{
    int Save();
    Task<int> SaveAsync();
    bool IsDisposed { get; }
}
