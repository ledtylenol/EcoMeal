public class Order
{
	public Guid Uid { get; set; }
	public Guid UserId { get; set; }
	public User? User { get; set; }
	public Guid BusinessId { get; set; }
	public Business? Business { get; set; }
	public Guid StatusId { get; set; }
	public Status? Status { get; set; }
	public uint OrderNumber { get; set; }

	public ICollection<OrderPackage> OrderPackages { get; set; } = [];
}
