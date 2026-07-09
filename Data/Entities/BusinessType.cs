using System.ComponentModel.DataAnnotations.Schema;

public class BusinessType
{
	public Guid Uid { get; set; }
	public string Name { get; set; }
	public ICollection<Business> Businesses { get; set; } = [];
}

public class BusinessTypeDTO
{
	public Guid Uid {get; set;}
	public string Name { get; set; }
}

