using System.ComponentModel.DataAnnotations.Schema;

public class PackageType
{
	public Guid Uid { get; set; }
	public string Name { get; set; }
	[InverseProperty("PackageType")]
	public ICollection<Package> Packages { get; set; } = [];
}

public class PackageTypeDTO
{
	public Guid? Uid { get; set; }
	public string Name { get; set; }
}
