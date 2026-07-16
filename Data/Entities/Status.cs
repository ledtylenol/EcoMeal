public class Status
{
	public Guid Uid { get; set; }
	public string? Name { get; set; }
	public ICollection<Order> Orders { get; set; } = [];
}
