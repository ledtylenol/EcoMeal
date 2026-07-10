using EcoMeal.Data;
using Microsoft.EntityFrameworkCore;

public class BusinessRepository(EcoMealDbContext context): BaseRepository<Business>(context)
{
}