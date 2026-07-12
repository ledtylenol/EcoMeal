using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController(
		UserManager<User> userManager,
		SignInManager<User> signInManager
		)
	: ControllerBase
{
	private readonly UserManager<User> _userManager = userManager;
	private readonly SignInManager<User> _signInManager = signInManager;


	[HttpPost("register")]
	public async Task<IActionResult> Register(UserDTO dto)
	{
		var user = new User
		{
			UserName = dto.Email,
			Email = dto.Email
		};

		Console.WriteLine($"Creating user {dto.Name} {dto.Email} ({dto.Password})");
		var result = await _userManager.CreateAsync(user, dto.Password);
		if (!result.Succeeded)
		{
			foreach (var error in result.Errors)
			{
				Console.WriteLine($"[Register Error] {error.Code}: {error.Description}");
			}
			return BadRequest(result.Errors);
		}


		Console.WriteLine("Successfully created user");
		result = await _userManager.AddToRoleAsync(user, dto.RoleName);
		if (!result.Succeeded)
		{
			foreach (var error in result.Errors)
			{
				Console.WriteLine($"[Role Error] {error.Code}: {error.Description}");
			}
			return BadRequest(result.Errors);
		}
		await _signInManager.SignInAsync(user, isPersistent: true);
		return Ok(new { user.Id });
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login(LoginDTO dto)
	{
		var user = await _userManager.FindByEmailAsync(dto.Email);
		if (user is null)
			return Unauthorized("Invalid email or password.");

		var result = await _signInManager.PasswordSignInAsync(user, dto.Password, isPersistent: true, lockoutOnFailure: false);
		if (!result.Succeeded)
			return Unauthorized("Invalid email or password.");

		return Ok();
	}
	[HttpPost("{id}/role")]
	public async Task<IActionResult> SetRole(Guid id, [FromBody] string roleName)
	{
		var user = await _userManager.FindByIdAsync(id.ToString());
		if (user is null) return NotFound();

		var currentRoles = await _userManager.GetRolesAsync(user);
		if (currentRoles.Count > 0)
			await _userManager.RemoveFromRolesAsync(user, currentRoles);

		var result = await _userManager.AddToRoleAsync(user, roleName);
		return result.Succeeded ? Ok() : BadRequest(result.Errors);
	}

	[HttpPost("logout")]
	public async Task<IActionResult> Logout()
	{
		await _signInManager.SignOutAsync();
		return RedirectToPage("/login");
	}


}
