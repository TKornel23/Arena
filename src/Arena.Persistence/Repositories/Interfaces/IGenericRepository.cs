namespace Arena.Persistence;

public interface IGenericRepository<T> where T : class
{
    void Update(T entry);
    Task<T?> Get(Guid guid);
    Task<IEnumerable<T>> Get(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "");
    Task Insert(T entity);
    Task<bool> Save();
    Task Insert(IEnumerable<T> entities);
}