using EcoMeal.Data;
using Microsoft.EntityFrameworkCore;

public class UserRepository(EcoMealDbContext context): BaseRepository<User>(context)
{
}