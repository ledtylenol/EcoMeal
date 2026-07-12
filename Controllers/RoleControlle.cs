using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class RoleController(RoleManager<IdentityRole<Guid>> roleManager) : ControllerBase
{
	private readonly RoleManager<IdentityRole<Guid>> _roleManager = roleManager;

	[HttpGet]
	public async Task<ActionResult<IEnumerable<IdentityRole<Guid>>>> GetRoles()
	{
		return await _roleManager.Roles.ToListAsync();
	}


}
