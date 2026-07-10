using EcoMeal.Data;
using Microsoft.EntityFrameworkCore;

public class RoleRepository(EcoMealDbContext context): BaseRepository<Role>(context)
{
}