public class BusinessStatus
{
	public Guid Uid { get; set; }
	public string? Name { get; set; }
	public ICollection<Business> Businesses { get; set; } = [];
}
