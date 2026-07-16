using EcoMeal.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class BusinessStatusController(EcoMealDbContext context) : ControllerBase
{
	private readonly BusinessStatusRepository repository = new(context);

	[HttpGet("{id}")]
	public async Task<ActionResult<BusinessStatus?>> GetStatus(Guid id)
	{
		return await repository.GetByIdAsync(id);
	}

	public async Task<BusinessStatus?> GetStatusAsync(Guid id)
	{
		return await repository.GetByIdAsync(id);
	}


}
