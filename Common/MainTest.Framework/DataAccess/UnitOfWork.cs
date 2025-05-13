using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MainTest.Framework.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly DbContext dbContext;
        public Guid UserId { get; }
        public UnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public UnitOfWork(DbContext dbContext, Guid userId)
        {
            this.dbContext = dbContext;
            this.UserId = userId;
        }

        public virtual int Save()
        {
            var result = dbContext.SaveChanges();
            return result;
        }

        public virtual async Task<int> SaveAsync()
        {
            var result = await dbContext.SaveChangesAsync();
            return result;
        }

        public bool IsDisposed { get; protected set; }


        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (dbContext != null)
                {
                    dbContext.Dispose();
                }
            }

            IsDisposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }


    }


    public static class UnitOfWorkExtensions
    {
        public static void AddUnitOfWork(this IServiceCollection services, Action<UnitOfWorkOptions> configureOptions)
        {
            services.AddScoped<IUnitOfWork>(serviceProvider =>
            {               
                var dbContext = serviceProvider.GetRequiredService<DbContext>();
                
                var options = new UnitOfWorkOptions();
                configureOptions(options);
                
                return new UnitOfWork(dbContext, options.UserId);
            });
        }
    }
}
