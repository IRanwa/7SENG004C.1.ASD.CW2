using System.Linq;
using System.Linq.Expressions;

namespace _7SENG004C._1.ASD.CW2;

public class UnitOfWork
{
    private static UnitOfWork unitOfWork;

    private ASDDbContext dbContext;

    private UnitOfWork()
    {
        dbContext = new ASDDbContext();
    }

    public static UnitOfWork Instance
    {
        get
        {
            unitOfWork ??= new UnitOfWork();
            return unitOfWork;
        }
    }

    public void SaveChanges()
    {
        dbContext.SaveChanges();
    }

    public void AddEntity<TEntity>(TEntity entity)
    {
        dbContext.Add(entity);
    }

    public void UpdateEntity<TEntity>(TEntity entity)
    {
        dbContext.Update(entity);
    }

    public TEntity GetOne<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        return dbContext.Set<TEntity>().FirstOrDefault(predicate);
    }

    public IQueryable<TEntity> GetAll<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        return dbContext.Set<TEntity>().Where(predicate);
    }
}