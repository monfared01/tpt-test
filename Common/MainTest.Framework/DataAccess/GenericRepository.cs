using MainTest.Framework.Entity;
using MainTest.Framework.Extensions;
using MainTest.Framework.Mapping.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq.Expressions;

namespace MainTest.Framework.DataAccess
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        protected DbContext Context { get; }
        protected DbSet<T> DbSet { get; }
        protected  ICollection<PermittedEntity> PermittedEntities;

        public GenericRepository(DbContext context, ICollection<PermittedEntity> permittedEntities) : base()
        {
            PermittedEntities = permittedEntities;
            Context = context;
            DbSet = Context.Set<T>();
        }

        public GenericRepository(DbContext context) : base()
        {
            if (typeof(ISecureEntity).IsAssignableFrom(typeof(T)))
            {
                throw new InvalidOperationException("For secure entities, permitted entities must be declared");
            }
            Context = context;
            DbSet = Context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? skip = null, int? take = null, params Expression<Func<T, object>>[] navigationProperties)
        {
            return GetQueryable(null, orderBy, skip, take, navigationProperties).AsParallel().ToList();
        }
        public virtual IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? skip = null, int? take = null, params string[] navigationProperties)
        {
            return GetQueryableStringNavProps(null, orderBy, skip, take, navigationProperties).AsParallel().ToList();
        }
        public virtual IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? skip = null, int? take = null)
        {
            return GetQueryable(null, orderBy, skip, take, null).AsParallel().ToList();
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null,
            params Expression<Func<T, object>>[] navigationProperties)
        {
            return GetQueryable(filter, orderBy, skip, take, navigationProperties).ToList();
        }
        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null,
            params string[] navigationProperties)
        {
            return GetQueryableStringNavProps(filter, orderBy, skip, take, navigationProperties).ToList();
        }
        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null)
        {
            return GetQueryable(filter, orderBy, skip, take, null).ToList();
        }

        public virtual T GetOne(Expression<Func<T, bool>> filter = null,
            params Expression<Func<T, object>>[] navigationProperties)
        {
            return GetQueryable(filter, null, null, null, navigationProperties).FirstOrDefault();
        }
        public virtual T GetOne(Expression<Func<T, bool>> filter = null,
            params string[] navigationProperties)
        {
            return GetQueryableStringNavProps(filter, null, null, null, navigationProperties).FirstOrDefault();
        }
        public virtual T GetOne(Expression<Func<T, bool>> filter = null)
        {
            return GetQueryable(filter, null, null, null, null).FirstOrDefault();
        }

        public virtual T GetFirst(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] navigationProperties)
        {
            return GetQueryable(filter, orderBy, null, null, navigationProperties).FirstOrDefault();
        }
        public virtual T GetFirst(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params string[] navigationProperties)
        {
            return GetQueryableStringNavProps(filter, orderBy, null, null, navigationProperties).FirstOrDefault();
        }
        public virtual T GetFirst(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            return GetQueryable(filter, orderBy, null, null, null).FirstOrDefault();
        }

        public virtual T? GetById(Guid id)
        {
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return DbSet.Find(id);
        }

        public virtual Int64 GetCount(Expression<Func<T, bool>> filter = null)
        {
            return GetQueryable(filter).Count();
        }

        public virtual Int64 GetSum(Expression<Func<T, Int64>> selector, Expression<Func<T, bool>> filter = null)
        {
            return GetQueryable(filter).Sum(selector);
        }

        public virtual Int64 GetMax(Expression<Func<T, Int64>> selector, Expression<Func<T, bool>> filter = null)
        {
            return GetQueryable(filter).Max(selector);
        }

        public virtual Int64 GetMin(Expression<Func<T, Int64>> selector, Expression<Func<T, bool>> filter = null)
        {
            return GetQueryable(filter).Min(selector);
        }

        public virtual bool GetExists(Expression<Func<T, bool>> filter = null)
        {
            return GetQueryable(filter).Any();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null,
            params Expression<Func<T, object>>[] navigationProperties)
        {
            return await GetQueryable(null, orderBy, skip, take, navigationProperties)
                .ToListAsync();
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null,
            params string[] navigationProperties)
        {
            return await GetQueryableStringNavProps(null, orderBy, skip, take, navigationProperties)
                .ToListAsync();
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null)
        {
            return await GetQueryable(null, orderBy, skip, take, null).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null,
            params Expression<Func<T, object>>[] navigationProperties)
        {
            return await GetQueryable(filter, orderBy, skip, take, navigationProperties).ToListAsync().ConfigureAwait(false);
        }
        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null,
        params string[] navigationProperties)
        {
            return await GetQueryableStringNavProps(filter, orderBy, skip, take, navigationProperties).ToListAsync().ConfigureAwait(false);
        }
        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null)
        {
            return await GetQueryable(filter, orderBy, skip, take, null).ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<T> GetOneAsync(Expression<Func<T, bool>> filter = null,
            params Expression<Func<T, object>>[] navigationProperties)
        {
            return await GetQueryable(filter, null, null, null, navigationProperties).FirstOrDefaultAsync();
        }
        public virtual async Task<T> GetOneAsync(Expression<Func<T, bool>> filter = null,
            params string[] navigationProperties)
        {
            return await GetQueryableStringNavProps(filter, null, null, null, navigationProperties).FirstOrDefaultAsync();
        }
        public virtual async Task<T> GetOneAsync(Expression<Func<T, bool>> filter = null)
        {
            return await GetQueryable(filter, null, null, null, null).FirstOrDefaultAsync();
        }

        public virtual async Task<T> GetFirstAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] navigationProperties)
        {
            return await GetQueryable(filter, orderBy, null, null, navigationProperties).FirstOrDefaultAsync();
        }
        public virtual async Task<T> GetFirstAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params string[] navigationProperties)
        {
            return await GetQueryableStringNavProps(filter, orderBy, null, null, navigationProperties).FirstOrDefaultAsync();
        }
        public virtual async Task<T> GetFirstAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            return await GetQueryable(filter, orderBy, null, null, null).FirstOrDefaultAsync();
        }

        public virtual Task<T?> GetByIdAsync(Guid id)
        {
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return DbSet.FindAsync(id).AsTask();
        }

        public virtual Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null)
        {
            return GetQueryable(filter).CountAsync();
        }

        public virtual Task<int> GetSumAsync(Expression<Func<T, int>> selector, Expression<Func<T, bool>> filter = null)
        {
            return GetQueryable(filter).SumAsync(selector);
        }

        public virtual Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter = null)
        {
            return GetQueryable(filter).AnyAsync();
        }

        public virtual void Create(T entity)
        {
            if (typeof(IReadonlyEntity).IsAssignableFrom(typeof(T)))
            {
                throw new Exception("The operation is not accessible for read-only entities");
            }

            if (typeof(ISecureEntity).IsAssignableFrom(typeof(T)))
            {
                var entityType = (entity as ISecureEntity)?.EntityType;

                if (entityType == null || !PermittedEntities.Any(q => q.Method == CrudMethods.Create && q.EntityType == entityType))
                {
                    throw new Exception("Access Denied");
                }
            }
            DbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            if (typeof(IReadonlyEntity).IsAssignableFrom(typeof(T)))
            {
                throw new Exception("The operation is not accessible for read-only entities");
            }

            if (typeof(ISecureEntity).IsAssignableFrom(typeof(T)))
            {
                var entityType = (entity as ISecureEntity)?.EntityType;

                if (entityType == null || !PermittedEntities.Any(q => q.Method == CrudMethods.Edit && q.EntityType == entityType))
                {
                    throw new Exception("Access Denied");
                }
            }
            DbSet.Update(entity);

        }

        public virtual void Delete(Guid id)
        {
            if (typeof(IReadonlyEntity).IsAssignableFrom(typeof(T)))
            {
                throw new Exception("The operation is not accessible for read-only entities");
            }
            T entity = DbSet.Find(id);

            if (typeof(ISecureEntity).IsAssignableFrom(typeof(T)))
            {
                var entityType = (entity as ISecureEntity)?.EntityType;

                if (entityType == null || !PermittedEntities.Any(q => q.Method == CrudMethods.Delete && q.EntityType == entityType))
                {
                    throw new Exception("Access Denied");
                }
            }
            if (entity != null) Delete(entity);
        }

        public virtual void Delete(T entity)
        {
            if (typeof(IReadonlyEntity).IsAssignableFrom(typeof(T)))
            {
                throw new Exception("The operation is not accessible for read-only entities");
            }

            if (typeof(ISecureEntity).IsAssignableFrom(typeof(T)))
            {
                var entityType = (entity as ISecureEntity)?.EntityType;

                if (entityType == null || !PermittedEntities.Any(q => q.Method == CrudMethods.Delete && q.EntityType == entityType))
                {
                    throw new Exception("Access Denied");
                }
            }

            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            DbSet.Remove(entity);
        }

        protected virtual IQueryable<T> GetQueryable(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = Context.Set<T>();

            Expression<Func<T, bool>> entityFilter = null;
            if (typeof(ISecureEntity).IsAssignableFrom(typeof(T)))
            {
                entityFilter = p => PermittedEntities.Any(q => q.Method == CrudMethods.Read && q.EntityType == (p as ISecureEntity).EntityType);
            }

            if (filter != null)
            {
                query = entityFilter != null ? query.Where(entityFilter.And(filter)) : query.Where(filter);
            }
            else if (entityFilter != null)
            {
                query = query.Where(entityFilter);
            }

            if (navigationProperties != null)
                query = navigationProperties
                    .Aggregate(query, (current, navigationProperty) => current.Include(navigationProperty));

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query
                .AsQueryable()
                .AsNoTracking();
        }
        protected virtual IQueryable<T> GetQueryableStringNavProps(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? skip = null,
            int? take = null,
            params string[] navigationProperties)
        {
            bool shouldTrack = false; 
            IQueryable<T> query = Context.Set<T>();

            Expression<Func<T, bool>> entityFilter = null;
            if (typeof(ISecureEntity).IsAssignableFrom(typeof(T)))
            {
                entityFilter = p => PermittedEntities.Any(q => q.Method == CrudMethods.Read && q.EntityType == (p as ISecureEntity).EntityType);
            }

            if (filter != null)
            {
                query = entityFilter != null ? query.Where(entityFilter.And(filter)) : query.Where(filter);
            }
            else if (entityFilter != null)
            {
                query = query.Where(entityFilter);
            }

            if (navigationProperties != null)
            {
                query = navigationProperties
                    .Aggregate(query, (current, navigationProperty) => current.Include(navigationProperty));
                foreach (var s in navigationProperties)
                    shouldTrack &= s.IndexOf(".") > 0;
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            if (shouldTrack)
                query = query.AsTracking();
            else
                query = query.AsNoTracking();

            return query
                .AsQueryable();
        }
    }
}
