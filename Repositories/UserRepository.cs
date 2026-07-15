using EcoMeal.Data;

public class UserRepository(EcoMealDbContext context) : BaseRepository<User>(context)
{
}
