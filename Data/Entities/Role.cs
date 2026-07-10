using System.ComponentModel.DataAnnotations.Schema;

public class Role
{
	public Guid Uid { get; set; }
	public string Name { get; set; }

 	[InverseProperty("Role")]
	public ICollection<User> Users { get; set; } = [];
}
public class RoleDTO
{
	public Guid? Uid { get; set; }
	public string Name { get; set; }
}
