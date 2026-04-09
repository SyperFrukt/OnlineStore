using Microsoft.EntityFrameworkCore;
using OnlineStore.Infrastructure.Data;
using System.Linq.Expressions;

namespace OnlineStore.Infrastructure.Repositories;

/// <summary>
/// Универсальный репозиторий для базовых CRUD-операций.
/// Реализует требования к слою доступа к данным через EF Core.
/// </summary>
public class GenericRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    /// <summary>Получить все записи</summary>
    public virtual async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    /// <summary>Получить запись по ID</summary>
    public virtual async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    /// <summary>Добавить новую запись</summary>
    public virtual async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>Обновить существующую запись</summary>
    public virtual async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>Удалить запись по ID</summary>
    public virtual async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>Поиск по условию</summary>
    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
        await _dbSet.Where(predicate).ToListAsync();
}
