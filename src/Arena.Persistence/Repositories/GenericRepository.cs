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

    public void Update(T entry)
    {
        dbSet.Attach(entry);
        context.Entry(entry).State = EntityState.Modified;
    }

    public async Task<T?> Get(Guid guid)
    {
        return await this.dbSet.FindAsync(guid);
    }

    public virtual async Task<IEnumerable<T>> Get(
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

        query = query.AsNoTracking();

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }

    public async Task Insert(T entity)
    {
        await this.dbSet.AddAsync(entity);
    }

    public async Task Insert(IEnumerable<T> entities)
    {
        await this.dbSet.AddRangeAsync(entities);
    }

    public async Task<bool> Save()
    {
        return await this.context.SaveChangesAsync() > 0;
    }
}
