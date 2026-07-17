using EcoMeal.Data;

public class OrderRepository(EcoMealDbContext context) : BaseRepository<Order>(context), IOrderRepository
{
	public Guid CancelledGuid { get; set; } = new("0223b286-0132-4783-a5bd-43224d996e50");
	public Status? cancelledStatus = null;

	public async Task CancelOrder(Order order)
	{
		cancelledStatus ??= await context.Set<Status>().FindAsync(CancelledGuid);
		if (order.StatusId == CancelledGuid) return;
		foreach (var op in order.OrderPackages)
		{
			var package = await context.Set<Package>().FindAsync(op.PackageId);
			if (package is null) continue;
			package.Quantity += op.Quantity;

		}
		order.Status = cancelledStatus;
		await UpdateAsync(order, (Guid)order.Uid!);

	}
	public override async Task<Order?> AddAsync(Order order)
	{
		order.Uid ??= Guid.NewGuid();
		order.OrderDate = DateTime.Now;

		foreach (var op in order.OrderPackages)
		{
			var package = await context.Set<Package>().FindAsync(op.PackageId);
			if (package is null) return null;

			if (package.Quantity < op.Quantity) return null; // optional: guard against overselling

			package.Quantity -= op.Quantity;
		}

		await context.SaveChangesAsync(); // persist the quantity decrements
		await base.AddAsync(order);
		return order;
	}
}
