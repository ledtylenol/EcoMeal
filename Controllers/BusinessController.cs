using EcoMeal.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class BusinessController(EcoMealDbContext context, IWebHostEnvironment _env) : ControllerBase
{
	private BusinessRepository repository = new(context);
	private IWebHostEnvironment env = _env;

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Business>>> GetBusinesses()
	{
		return await repository.GetAllAsync();
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Business>> GetBusinesses(Guid id)
	{
		var business = await repository.GetByIdAsync(id);
		if (business == null) return NoContent();
		return business;
	}

	[HttpPost]
	public async Task<ActionResult<Business>> PostBusiness(BusinessDTO business)
	{
		var _business = new Business
		{
			Uid = new Guid(),
			Name = business.Name,
			Address = business.Address,
			Description = business.Description,
			ImageUrl = business.ImageUrl,
			BusinessTypeId = business.BusinessTypeId

		};
		await repository.AddAsync(_business);

		return CreatedAtAction(
				nameof(GetBusinesses),
				new { id = _business.Uid }
				);
	}

	[HttpPut]
	public async Task<ActionResult<Business>> PutBusiness(BusinessDTO business)
	{
		if (business.Uid is null ) return NoContent();
		var _business = await repository.GetByIdAsync((Guid)business.Uid);
		if (_business is null) return NoContent();

		_business.Name = business.Name;
		_business.Description = business.Description;
		_business.ImageUrl = business.ImageUrl;
		_business.BusinessTypeId = business.BusinessTypeId;

		await repository.UpdateAsync(_business);

		return CreatedAtAction(
				nameof(GetBusinesses),
				new { id = business.Uid }
				);
	}
	[HttpDelete("{guid}")]
	public async Task<ActionResult<Business>> DeleteBusiness(Guid guid)
	{
		var business = await repository.GetByIdAsync(guid);
		if (business is null) return NotFound();
		await repository.DeleteAsync(business);
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
		var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "businesses");
		Console.WriteLine("Uploading...");
		Directory.CreateDirectory(uploadsFolder); 

		var filePath = Path.Combine(uploadsFolder, fileName);
		using (var stream = new FileStream(filePath, FileMode.Create))
		{
			await file.CopyToAsync(stream);
		}

		var relativeUrl = $"/uploads/{fileName}";
		return Ok(new { url = relativeUrl });
	}

}
