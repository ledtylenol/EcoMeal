using System.ComponentModel.DataAnnotations.Schema;

public class Business
{
	public Guid Uid { get; set; }
	public Guid OwnerId { get; set; }
	public User? Owner { get; set; }
	public string? Name { get; set; }
	public string? Description { get; set; }
	public string? Address { get; set; }
	public string? ImageUrl { get; set; }
	public Guid BusinessTypeId { get; set; }

	public BusinessType? BusinessType { get; set; }

	public ICollection<Package> Packages { get; set; } = [];
	public ICollection<Order> Orders { get; set; } = [];
}

public class BusinessDTO
{

	public BusinessDTO()
	{
	}
	public BusinessDTO(Business business)
	{
		Name = business.Name;
		Description = business.Description;
		Address = business.Address;
		ImageUrl = business.ImageUrl;
		BusinessTypeId = business.BusinessTypeId;
	}


	public Guid? Uid { get; set; }
	public Guid OwnerId { get; set; }
	public string? Name { get; set; }
	public string? Description { get; set; }
	public string? Address { get; set; }
	public string? ImageUrl { get; set; }
	public Guid BusinessTypeId { get; set; }
}
