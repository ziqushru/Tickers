using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public abstract class BaseRepository<T, R> : IRepositoryBase<T> where T : BaseModel where R : DbContext
{
    private readonly R context;
    
    protected BaseRepository(R context)
    {
        this.context = context;
    }
    
    public void insert(T entity)
    {
        this.context.Set<T>().Add(entity);
        this.context.SaveChanges();
    }
    
    public IQueryable<T> select(bool track_changes) =>
        !track_changes ?
            this.context.Set<T>()
                .AsNoTracking():
            this.context.Set<T>();
    
    public IQueryable<T> selectByCondition(Expression<Func<T, bool>> expression, bool track_changes) =>
        !track_changes ?
            this.context.Set<T>()
                .Where(expression)
                .AsNoTracking(): 
            this.context.Set<T>()
                .Where(expression);
    
    public void update(T entity)
    {
        this.context.Set<T>().Update(entity);
        this.context.SaveChanges();
    }
    
    public void delete(T entity)
    {
        entity.is_deleted = true;
        entity.delete_date = DateTime.Now;
        this.context.Set<T>().Update(entity);
        this.context.SaveChanges();
    }
    
    public async Task insertAsync(T entity)
    {
        this.context.Set<T>().Add(entity);
        await this.context.SaveChangesAsync();
    }
    
    public async Task updateAsync(T entity)
    {
        entity.update_date = DateTime.Now;
        this.context.Set<T>().Update(entity);
        await this.context.SaveChangesAsync();
    }
    
    public async Task deleteAsync(T entity)
    {
        entity.is_deleted = true;
        entity.delete_date = DateTime.Now;
        this.context.Set<T>().Update(entity);
        await this.context.SaveChangesAsync();
    }
}
