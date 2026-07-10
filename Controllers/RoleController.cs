using EcoMeal.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class RoleController(EcoMealDbContext context) : ControllerBase
{
	private RoleRepository repository = new(context);

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
	{
		return await repository.GetAllAsync();
	}



	[HttpGet("{id}")]
	public async Task<ActionResult<Role>> GetRole(Guid id)
	{
		var role = await repository.GetByIdAsync(id);
		if (role is null) return NoContent();
		return role;
	}

	[HttpPost]
	public async Task<ActionResult<Role>> PostRole(RoleDTO role)
	{
		var _role = new Role
		{
			Uid = new Guid(),
			Name = role.Name,
		};
		await repository.AddAsync(_role);

		return CreatedAtAction(
				nameof(GetRoles),
				new { id = _role.Uid }
				);
	}

	[HttpPut]
	public async Task<ActionResult<Business>> PutRole(RoleDTO role)
	{
		if (role.Uid is null ) return NoContent();
		var _role = await repository.GetByIdAsync((Guid)role.Uid);
		if (_role is null) return NoContent();

        _role.Name = role.Name;

		await repository.UpdateAsync(_role);

		return CreatedAtAction(
				nameof(GetRoles),
				new { id = _role.Uid }
				);
	}
	[HttpDelete("{guid}")]
	public async Task<ActionResult<Role>> DeletePackage(Guid guid)
	{
		var package = await repository.GetByIdAsync(guid);
		if (package is null) return NotFound();
		await repository.DeleteAsync(package);
		return NoContent();
	}
}
