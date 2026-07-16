using EcoMeal.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/business_type")]
[ApiController]
public class BusinessTypeController(EcoMealDbContext context) : ControllerBase
{
	private readonly BusinessTypeRepository repository = new(context);

	[HttpGet]
	public async Task<ActionResult<IEnumerable<BusinessType>>> GetBusinessTypes()
	{
		return await repository.GetAllAsync();
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<BusinessType>> GetBusinessType(Guid id)
	{
		var businessType = await repository.GetByIdAsync(id);
		if (businessType == null) return NoContent();
		return businessType;
	}

	[HttpPost]
	public async Task<ActionResult<BusinessType>> PostBusinessType(BusinessTypeDTO businessType)
	{
		var _businessType = new BusinessType
		{
			Uid = new Guid(),
			Name = businessType.Name,
		};
		await repository.AddAsync(_businessType);

		return CreatedAtAction(
				nameof(GetBusinessTypes),
				new { id = _businessType.Uid }
				);
	}
	[HttpPut]
	public async Task<ActionResult<BusinessType>> PutBusinessType(BusinessType businessType)
	{
		await repository.UpdateAsync(businessType, businessType.Uid);

		return CreatedAtAction(
				nameof(GetBusinessTypes),
				new { id = businessType.Uid }
				);
	}

	[HttpDelete("{guid}")]
	public async Task<ActionResult<BusinessType>> DeleteBusinessType(Guid guid)
	{
		var businessType = await repository.GetByIdAsync(guid);
		if (businessType is null) return NotFound();
		await repository.DeleteAsync(businessType);
		return NoContent();
	}
}
