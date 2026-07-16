using EcoMeal.Data;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class OrderController(EcoMealDbContext context) : ControllerBase
{
	private readonly OrderRepository repository = new(context);
	private readonly PackageRepository packageRepository = new(context);

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
	{
		return await repository.GetAllAsync();
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Order?>> GetOrder(Guid id)
	{
		var Order = await repository.GetByIdAsync(id);
		if (Order == null) return NoContent();
		return Order;
	}

	[HttpPost]
	public async Task<ActionResult<Order>> PostOrder(Order order)
	{
		order.Uid ??= Guid.NewGuid();


		foreach (var packageInfo in order.OrderPackages.Select(op => new { op.Package, op.Quantity }))
		{
			var (package, quantity) = (packageInfo.Package, packageInfo.Quantity);
			if (package is null) return NoContent();
			package.Quantity -= quantity;
			await packageRepository.UpdateAsync(package, package.Uid);


		}
		await repository.AddAsync(order);
		return CreatedAtAction(
				nameof(GetOrders),
				new { id = order.Uid }
				);
	}

	[HttpPut]
	public async Task<ActionResult<Order>> PutOrder(OrderDTO order)
	{
		if (order.Uid is null) return NoContent();
		var _order = await repository.GetByIdAsync((Guid)order.Uid);
		if (_order is null) return NoContent();

		_order.BusinessId = (Guid)order.BusinessId!;
		_order.UserId = (Guid)order.UserId!;
		_order.OrderNumber = order.OrderNumber;

		await repository.UpdateAsync(_order, (Guid)_order.Uid!);

		return CreatedAtAction(
				nameof(GetOrders),
				new { id = _order.Uid }
				);
	}
	[HttpDelete("{guid}")]
	public async Task<ActionResult<Order>> DeleteOrder(Guid guid)
	{
		var Order = await repository.GetByIdAsync(guid);
		if (Order is null) return NotFound();
		await repository.DeleteAsync(Order);
		return NoContent();
	}


}
