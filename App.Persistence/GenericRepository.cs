using System.Linq.Expressions;
using App.Application.Contracts.Persistence;
using App.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence;

public class GenericRepository<T, TId>(AppDbContext context) : IGenericRepository<T, TId> where T : BaseEntity<TId> where TId : struct
{
	protected AppDbContext Context = context;
	private readonly DbSet<T> _dbSet = context.Set<T>();

	public Task<bool> AnyAsync(TId id) => _dbSet.AnyAsync(e => e.Id.Equals(id));
	public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => _dbSet.AnyAsync(predicate);
	public Task<List<T>> GetAllAsync() => _dbSet.ToListAsync();
	public Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize) => _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
	public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate).AsNoTracking();
	public ValueTask<T?> GetByIdAsync(int id) => _dbSet.FindAsync(id);
	public async ValueTask AddAsync(T entity) => await _dbSet.AddAsync(entity);
	public void Update(T entity) => _dbSet.Update(entity);
	public void Delete(T entity) => _dbSet.Remove(entity);
}