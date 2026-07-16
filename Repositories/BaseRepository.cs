using Microsoft.EntityFrameworkCore;
namespace EcoMeal.Data
{
	public class BaseRepository<TEntity>(EcoMealDbContext context) : IRepository<TEntity> where TEntity : class
	{
		public virtual async Task<TEntity?> GetByIdAsync(Guid id)
		{
			return await context.Set<TEntity>().FindAsync(id);
		}

		public async Task<List<TEntity>> GetAllAsync()
		{
			return await context.Set<TEntity>().ToListAsync();
		}

		public async Task<TEntity> AddAsync(TEntity entity)
		{

			await context.Set<TEntity>().AddAsync(entity);
			await context.SaveChangesAsync();
			return entity;
		}

		public async Task<TEntity?> UpdateAsync(TEntity entity, Guid uid)
		{

			var existing = await context.Set<TEntity>().FindAsync(uid);
			if (existing is null) return null;

			context.Entry(existing).CurrentValues.SetValues(entity);
			await context.SaveChangesAsync();
			return entity;
		}

		public async Task DeleteAsync(TEntity entity)
		{
			context.Set<TEntity>().Remove(entity);
			await context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Guid id)
		{
			var entity = await context.Set<TEntity>().FindAsync(id);
			if (entity is not null)
			{
				context.Set<TEntity>().Remove(entity);
				await context.SaveChangesAsync();
			}
		}
	}
}
