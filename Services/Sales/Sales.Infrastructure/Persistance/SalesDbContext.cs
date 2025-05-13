using MainTest.Framework.Entity;
using Microsoft.EntityFrameworkCore;
using Sales.Domain;
using System.Reflection;

namespace Sales.Infrastructure.Persistance
{
    public class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public Guid UserId { get; set; }
   
        public DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.Now;
                        entry.Entity.CreatedBy = this.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = DateTime.Now;
                        entry.Entity.LastModifiedBy = this.UserId;
                        break;
                }
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.Now;
                        entry.Entity.CreatedBy = this.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = DateTime.Now;
                        entry.Entity.LastModifiedBy = this.UserId;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
