using System.Linq.Expressions;

namespace _7SENG004C._1.ASD.CW2;

/// <summary>
/// The unit of work.
/// </summary>
public class UnitOfWork
{
    /// <summary>
    /// The unit of work.
    /// </summary>
    private static UnitOfWork unitOfWork;

    /// <summary>
    /// The database context.
    /// </summary>
    private ASDDbContext dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
    /// </summary>
    private UnitOfWork()
    {
        dbContext = new ASDDbContext();
    }

    /// <summary>
    /// The instance.
    /// </summary>
    public static UnitOfWork Instance
    {
        get
        {
            unitOfWork ??= new UnitOfWork();
            return unitOfWork;
        }
    }

    /// <summary>
    /// The save changes.
    /// </summary>
    public void SaveChanges()
    {
        dbContext.SaveChanges();
    }

    /// <summary>
    /// The add entity.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <param name="entity">The entity.</param>
    public void AddEntity<TEntity>(TEntity entity)
    {
        dbContext.Add(entity);
    }

    /// <summary>
    /// The update entity.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <param name="entity">The entity.</param>
    public void UpdateEntity<TEntity>(TEntity entity)
    {
        dbContext.Update(entity);
    }

    /// <summary>
    /// The get one record.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <param name="predicate">The predicate.</param>
    /// <returns>returns entity.</returns>
    public TEntity GetOne<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        return dbContext.Set<TEntity>().FirstOrDefault(predicate);
    }

    /// <summary>
    /// The get all records.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <param name="predicate">The predicate.</param>
    /// <returns>Returns entity list.</returns>
    public IQueryable<TEntity> GetAll<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        return dbContext.Set<TEntity>().Where(predicate);
    }
}