using EcoMeal.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class PackageController(EcoMealDbContext context, IWebHostEnvironment _env) : ControllerBase
{
	private PackageRepository repository = new(context);
	private IWebHostEnvironment env = _env;

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Package>>> GetPackages()
	{
		return await repository.GetAllAsync();
	}


	[HttpGet("business/{id}")]
	public async Task<ActionResult<IEnumerable<Package>>> GetPackagesFromBusiness(Guid id)
	{
		return await repository.GetFromBusinessId(id);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Package>> GetPackage(Guid id)
	{
		var business = await repository.GetByIdAsync(id);
		if (business == null) return NoContent();
		return business;
	}

	[HttpPost]
	public async Task<ActionResult<Package>> PostPackage(PackageDTO package)
	{
		var _package = new Package
		{
			Uid = new Guid(),
			Name = package.Name,
			ImageUrl = package.ImageUrl,
			Description = package.Description,
			BusinessId = package.BusinessId,
			PackageTypeId = package.PackageTypeId,
			Price = package.Price,
			Quantity = package.Quantity,
			PickupEnd = package.PickupEnd,
			PickupStart = package.PickupStart

		};
		await repository.AddAsync(_package);

		return CreatedAtAction(
				nameof(GetPackages),
				new { id = _package.Uid }
				);
	}

	[HttpPut]
	public async Task<ActionResult<Business>> PutPackage(PackageDTO package)
	{
		if (package.Uid is null) return NoContent();
		var _package = await repository.GetByIdAsync((Guid)package.Uid);
		if (_package is null) return NoContent();

		_package.Name = package.Name;
		_package.ImageUrl = package.ImageUrl;
		_package.Description = package.Description;
		_package.BusinessId = package.BusinessId;
		_package.PackageTypeId = package.PackageTypeId;
		_package.Price = package.Price;
		_package.Quantity = package.Quantity;
		_package.PickupEnd = package.PickupEnd;
		_package.PickupStart = package.PickupStart;

		await repository.UpdateAsync(_package, _package.Uid);

		return CreatedAtAction(
				nameof(GetPackages),
				new { id = _package.Uid }
				);
	}
	[HttpDelete("{guid}")]
	public async Task<ActionResult<Package>> DeletePackage(Guid guid)
	{
		var package = await repository.GetByIdAsync(guid);
		if (package is null) return NotFound();
		await repository.DeleteAsync(package);
		return NoContent();
	}

	[HttpPost("upload_image")]
	public async Task<IActionResult> UploadImage(IFormFile file)
	{
		if (file == null || file.Length == 0)
			return BadRequest("No file uploaded.");

		var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
		var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
		if (!allowedExtensions.Contains(ext))
			return BadRequest("Invalid file type.");

		var fileName = $"{Guid.NewGuid()}{ext}";
		var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "packages");
		Console.WriteLine("Uploading...");
		Directory.CreateDirectory(uploadsFolder);

		var filePath = Path.Combine(uploadsFolder, fileName);
		using (var stream = new FileStream(filePath, FileMode.Create))
		{
			await file.CopyToAsync(stream);
		}

		var relativeUrl = $"/uploads/packages/{fileName}";
		return Ok(new { url = relativeUrl });
	}

}
