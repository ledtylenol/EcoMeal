using System.ComponentModel.DataAnnotations.Schema;

public class User
{
	public Guid Uid { get; set; }
	public Guid RoleId { get; set; }
	[InverseProperty("Users")]
	public Role? Role { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public string Name { get; set; }
	public ICollection<Order> Orders { get; set; } = [];
}
