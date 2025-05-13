using MainTest.Framework.Entity;
using System.Linq.Expressions;

namespace MainTest.Framework.DataAccess;

public interface IGenericRepository<T> where T : IEntity
{
    IEnumerable<T> GetAll(
    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
    int? skip = null,
    int? take = null,
    params Expression<Func<T, object>>[] navigationProperties);
    IEnumerable<T> GetAll(
    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
    int? skip = null,
    int? take = null,
    params string[] navigationProperties);
    IEnumerable<T> GetAll(
    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
    int? skip = null,
    int? take = null);

    IEnumerable<T> Get(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        int? skip = null,
        int? take = null,
        params Expression<Func<T, object>>[] navigationProperties);
    IEnumerable<T> Get(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        int? skip = null,
        int? take = null,
        params string[] navigationProperties);
    IEnumerable<T> Get(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        int? skip = null,
        int? take = null);

    T GetOne(
        Expression<Func<T, bool>> filter = null,
        params Expression<Func<T, object>>[] navigationProperties);
    T GetOne(
        Expression<Func<T, bool>> filter = null,
        params string[] navigationProperties);
    T GetOne(
        Expression<Func<T, bool>> filter = null);

    T GetFirst(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] navigationProperties);
    T GetFirst(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params string[] navigationProperties);
    T GetFirst(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

    T? GetById(Guid id);

    Task<IEnumerable<T>> GetAllAsync(
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        int? skip = null,
        int? take = null,
        params Expression<Func<T, object>>[] navigationProperties);
    Task<IEnumerable<T>> GetAllAsync(
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        int? skip = null,
        int? take = null,
        params string[] navigationProperties);
    Task<IEnumerable<T>> GetAllAsync(
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        int? skip = null,
        int? take = null);

    Int64 GetCount(Expression<Func<T, bool>> filter = null);
    Int64 GetSum(Expression<Func<T, Int64>> selector, Expression<Func<T, bool>> filter = null);
    Int64 GetMax(Expression<Func<T, Int64>> selector, Expression<Func<T, bool>> filter = null);
    Int64 GetMin(Expression<Func<T, Int64>> selector, Expression<Func<T, bool>> filter = null);
    bool GetExists(Expression<Func<T, bool>> filter = null);

    Task<IEnumerable<T>> GetAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        int? skip = null,
        int? take = null,
        params Expression<Func<T, object>>[] navigationProperties);
    Task<IEnumerable<T>> GetAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        int? skip = null,
        int? take = null,
        params string[] navigationProperties);
    Task<IEnumerable<T>> GetAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        int? skip = null,
        int? take = null);

    Task<T> GetOneAsync(
        Expression<Func<T, bool>> filter = null,
        params Expression<Func<T, object>>[] navigationProperties);
    Task<T> GetOneAsync(
        Expression<Func<T, bool>> filter = null,
        params string[] navigationProperties);
    Task<T> GetOneAsync(
        Expression<Func<T, bool>> filter = null);

    Task<T> GetFirstAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] navigationProperties);
    Task<T> GetFirstAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params string[] navigationProperties);
    Task<T> GetFirstAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

    Task<T?> GetByIdAsync(Guid id);

    Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null);
    Task<int> GetSumAsync(Expression<Func<T, int>> selector, Expression<Func<T, bool>> filter = null);

    Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter = null);
    void Create(T entity);
    void Update(T entity);
    void Delete(Guid id);
    void Delete(T entity);
}
