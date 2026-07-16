using EcoMeal.Data;

public class OrderRepository(EcoMealDbContext context) : BaseRepository<Order>(context), IOrderRepository
{
	public Guid CancelledGuid { get; set; } = new("0223b286-0132-4783-a5bd-43224d996e50");
	public Status? cancelledStatus = null;

	public async Task CancelOrder(Order order)
	{
		cancelledStatus ??= await context.Set<Status>().FindAsync(CancelledGuid);
		if (order.Status == cancelledStatus) return;
		foreach (var op in order.OrderPackages)
		{
			if (op.Package is null) continue;
			op.Package.Quantity += op.Quantity;

		}
		order.Status = cancelledStatus;
		await UpdateAsync(order, (Guid)order.Uid!);

	}
}
