using System.ComponentModel.DataAnnotations.Schema;

public class Package
{
	public Guid Uid { get; set; }
	public Guid BusinessId { get; set; }
	public Business? Business { get; set; } = null;
	public Guid PackageTypeId { get; set; }
	[InverseProperty("Packages")]
	public PackageType? PackageType { get; set; } = null;
	public string Name { get; set; }
	public string? Description { get; set; }
	public float Price { get; set; }
	public uint Quantity { get; set; }
	public DateTime PickupStart { get; set; }
	public DateTime PickupEnd { get; set; }
	public string? ImageUrl { get; set; }

	public ICollection<OrderPackage> OrderPackages { get; set; } = [];
}

public class PackageDTO
{
	public Guid? Uid { get; set; }
	public Guid BusinessId { get; set; }
	public Guid PackageTypeId { get; set; }
	public string Name { get; set; }
	public string? Description { get; set; }
	public float Price { get; set; }
	public uint Quantity { get; set; }
	public DateTime PickupStart { get; set; }
	public DateTime PickupEnd { get; set; }
	public string? ImageUrl { get; set; }
}
