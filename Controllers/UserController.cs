using EcoMeal.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class UserController(EcoMealDbContext context) : ControllerBase
{
	private UserRepository repository = new(context);

	[HttpGet]
	public async Task<ActionResult<IEnumerable<User>>> GetUsers()
	{
		return await repository.GetAllAsync();
	}



	[HttpGet("{id}")]
	public async Task<ActionResult<User>> GetUser(Guid id)
	{
		var role = await repository.GetByIdAsync(id);
		if (role is null) return NoContent();
		return role;
	}

	[HttpPost]
	public async Task<ActionResult<User>> PostUser(UserDTO role)
	{
		var _user = new User
		{
			Uid = new Guid(),
			Name = role.Name,
		};
		await repository.AddAsync(_user);

		return CreatedAtAction(
				nameof(GetUsers),
				new { id = _user.Uid }
				);
	}

	[HttpPut]
	public async Task<ActionResult<User>> PutUser(UserDTO user)
	{
		if (user.Uid is null ) return NoContent();
		var _user = await repository.GetByIdAsync((Guid)user.Uid);
		if (_user is null) return NoContent();

        _user.Name = user.Name;
        _user.Email = user.Email;
        _user.RoleId = user.RoleId;
        _user.Password = user.Password;

		await repository.UpdateAsync(_user);

		return CreatedAtAction(
				nameof(GetUsers),
				new { id = _user.Uid }
				);
	}
	[HttpDelete("{guid}")]
	public async Task<ActionResult<User>> DeleteUser(Guid guid)
	{
		var package = await repository.GetByIdAsync(guid);
		if (package is null) return NotFound();
		await repository.DeleteAsync(package);
		return NoContent();
	}
}
