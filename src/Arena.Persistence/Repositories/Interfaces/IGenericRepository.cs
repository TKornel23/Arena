namespace Arena.Persistence;

public interface IGenericRepository<T> where T : class
{
    T? Get(Guid guid);
    IEnumerable<T> Get(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "");
    void Insert(T entity);
    bool Save();
    void Insert(IEnumerable<T> entities);
}