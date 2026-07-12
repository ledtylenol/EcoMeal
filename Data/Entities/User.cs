using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<Guid>
{
	public ICollection<Order> Orders { get; set; } = [];
}


public class LoginDTO
{
	public string Email { get; set; }
	public string Password { get; set; }
}
public class UserDTO
{
	public Guid? Id { get; set; }
	public string RoleName { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public string Name { get; set; }
}
