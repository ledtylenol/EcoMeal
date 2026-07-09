public class OrderPackage
{
	public Guid OrderId { get; set; }
	public Order? Order { get; set; } = null;
	public Guid PackageId { get; set; }
	public Package? Package { get; set; } = null;
	public uint Quantity { get; set; }
}
