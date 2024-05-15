namespace Arena.Persistence;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ArenaContext context;
    private readonly DbSet<T> dbSet;

    public GenericRepository(ArenaContext dbContext)
    {
        this.context = dbContext;
        this.dbSet = context.Set<T>();
    }

    public T? Get(Guid guid)
    {
        return this.dbSet.Find(guid);
    }

    public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "")
    {
        IQueryable<T> query = this.dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return orderBy(query).ToList();
        }
        else
        {
            return query.ToList();
        }
    }

    public void Insert(T entity)
    {
        this.dbSet.Add(entity);
    }

    public void Insert(IEnumerable<T> entities)
    {
        this.dbSet.AddRange(entities);
    }

    public bool Save()
    {
        return this.context.SaveChanges() > 0;
    }
}
