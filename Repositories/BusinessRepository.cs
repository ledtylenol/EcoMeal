using EcoMeal.Data;
using Microsoft.EntityFrameworkCore;

public class BusinessRepository(EcoMealDbContext context) : BaseRepository<Business>(context)
{
	public virtual async Task<User?> GetOwnerFromId(Guid id)
	{
		return await context.Set<Business>().Where(b => b.OwnerId == id).Select(b => b.Owner).FirstAsync();
	}
}
