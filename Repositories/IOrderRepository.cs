public interface IOrderRepository : IRepository<Order>
{
	public Task CancelOrder(Order order);
}
