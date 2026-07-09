using EcoMeal.Data;

public class PackageRepository(EcoMealDbContext context): BaseRepository<Package>(context)
{

}