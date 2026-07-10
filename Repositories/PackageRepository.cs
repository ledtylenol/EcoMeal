using EcoMeal.Data;
using Microsoft.EntityFrameworkCore;

public class PackageRepository(EcoMealDbContext context): BaseRepository<Package>(context)
{
    public virtual async Task<List<Package>> GetFromBusinessId(Guid id)
    {
        return await context.Set<Package>().Where(p => p.BusinessId == id).ToListAsync();
    }
}