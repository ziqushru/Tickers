using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository;

public interface IRepositoryBase<T>
{
    void insert(T entity);
    IQueryable<T> select(bool track_changes);
    IQueryable<T> selectByCondition(Expression<Func<T, bool>> expression, bool track_changes);
    void update(T entity);
    void delete(T entity);
    Task insertAsync(T entity);
    Task updateAsync(T entity);
    Task deleteAsync(T entity);
}
