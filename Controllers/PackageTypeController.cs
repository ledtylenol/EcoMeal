using EcoMeal.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/package_type")]
[ApiController]
public class PackageTypeController(EcoMealDbContext context) : ControllerBase
{
	private readonly PackageTypeRepository repository = new(context);

	[HttpGet]
	public async Task<ActionResult<IEnumerable<PackageType>>> GetPackageTypes()
	{
		return await repository.GetAllAsync();
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<PackageType>> GetPackageType(Guid id)
	{
		var packageType = await repository.GetByIdAsync(id);
		if (packageType == null) return NoContent();
		return packageType;
	}

	[HttpPost]
	public async Task<ActionResult<PackageType>> PostPackageType(PackageTypeDTO packageType)
	{
		var _packageType = new PackageType
		{
			Uid = new Guid(),
			Name = packageType.Name,
		};
		await repository.AddAsync(_packageType);

		return CreatedAtAction(
				nameof(GetPackageTypes),
				new { id = _packageType.Uid }
				);
	}
	[HttpPut]
	public async Task<ActionResult<PackageType>> PutPackageType(PackageType packageType)
	{
		await repository.UpdateAsync(packageType);

		return CreatedAtAction(
				nameof(GetPackageTypes),
				new { id = packageType.Uid }
				);
	}
}
