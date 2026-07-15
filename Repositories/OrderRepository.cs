using EcoMeal.Data;

public class OrderRepository(EcoMealDbContext context) : BaseRepository<Order>(context)
{
}
