using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using EcoMeal;
using EcoMeal.Data;
using EcoMeal.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

internal class Program
{
	private static async Task Main(string[] args)
	{
		// Replace with your connection string.
		var connectionString = "server=localhost;user=root;password=123;database=ef";
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		var services = builder.Services;
		services.AddRazorComponents()
				.AddInteractiveServerComponents();

		services.AddBlazorBootstrap();
		services.AddRazorPages();

		var serverVersion = new MariaDbServerVersion(new Version(12, 3, 2));
		services.AddHttpClient();
		services.AddHttpContextAccessor();
		services.AddDbContext<EcoMealDbContext>(
				dbContextOptions =>
				{
					dbContextOptions.UseMySql(connectionString, serverVersion);
					dbContextOptions.LogTo(Console.WriteLine, LogLevel.Information)
					.EnableDetailedErrors();
					dbContextOptions.EnableSensitiveDataLogging();
				});
		services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
		services.AddScoped(sp => new HttpClient { BaseAddress = new("http://localhost:5198") });
		services.AddScoped<IRepository<BusinessStatus>, BusinessStatusRepository>();
		services.AddScoped<IRepository<Business>, BusinessRepository>();
		services.AddScoped<IRepository<User>, UserRepository>();
		services.AddScoped<IRepository<BusinessType>, BusinessTypeRepository>();
		services.AddScoped<IRepository<PackageType>, PackageTypeRepository>();
		services.AddScoped<IRepository<Package>, PackageRepository>();
		services.AddScoped<IRepository<OrderPackage>, OrderPackageRepository>();
		services.AddScoped<IOrderRepository, OrderRepository>();
		builder.Services.AddIdentityCore<User>(options =>
		{
			options.Password.RequireDigit = true;
			options.Password.RequiredLength = 8;
			options.Password.RequireNonAlphanumeric = false;
			options.User.RequireUniqueEmail = true;
		})
				.AddRoles<IdentityRole<Guid>>()
				.AddEntityFrameworkStores<EcoMealDbContext>()
				.AddSignInManager()
				.AddDefaultTokenProviders();
		services.AddAuthentication(IdentityConstants.ApplicationScheme)
				.AddCookie(IdentityConstants.ApplicationScheme, opts =>
						{
							opts.LoginPath = "/login";
						});
		services.AddAuthorization();


		services.AddScoped<IUserClaimsPrincipalFactory<User>, UserClaimsPrincipalFactory<User, IdentityRole<Guid>>>();
		services.AddCascadingAuthenticationState();
		var app = builder.Build();

		using (var scope = app.Services.CreateScope())
		{
			var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
			string[] roles = ["Admin", "BusinessOwner", "Customer"];

			foreach (var roleName in roles)
			{
				if (!await roleManager.RoleExistsAsync(roleName))
				{
					await roleManager.CreateAsync(new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = roleName });
				}
			}
		}
		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Error", createScopeForErrors: true);
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}
		app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
		app.UseHttpsRedirection();

		app.UseAuthentication();
		app.UseAuthorization();
		app.UseAntiforgery();
		app.MapStaticAssets();
		app.UseStaticFiles();
		app.MapRazorComponents<App>()
				.AddInteractiveServerRenderMode();

		app.MapControllers();
		app.MapPost("/login", async (HttpContext httpContext, [FromForm] string email, [FromForm] string password, [FromForm] string? returnUrl,
				SignInManager<User> signInManager, UserManager<User> userManager) =>
		{
			var user = await userManager.FindByEmailAsync(email);
			if (user is null)
				return Results.Redirect($"/login?error=1{(string.IsNullOrWhiteSpace(returnUrl) ? "" : $"&returnUrl={Uri.EscapeDataString(returnUrl)}")}");

			var result = await signInManager.PasswordSignInAsync(user, password, isPersistent: true, lockoutOnFailure: false);
			if (!result.Succeeded)
				return Results.Redirect($"/login?error=1{(string.IsNullOrWhiteSpace(returnUrl) ? "" : $"&returnUrl={Uri.EscapeDataString(returnUrl)}")}");

			var redirectTo = string.IsNullOrWhiteSpace(returnUrl) ? "/" : returnUrl;
			return Results.Redirect(redirectTo);
		}).DisableAntiforgery();

		app.MapRazorPages();
		app.MapPost("/logout", async (HttpContext httpContext, SignInManager<User> signInManager) =>
		{
			await signInManager.SignOutAsync();
			return Results.Redirect("/login");
		}).DisableAntiforgery();
		using (var scope = app.Services.CreateScope())
		{
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
			var businessTypeRepo = scope.ServiceProvider.GetRequiredService<IRepository<BusinessType>>();
			var packageTypeRepo = scope.ServiceProvider.GetRequiredService<IRepository<PackageType>>();
			var businessStatusRepo = scope.ServiceProvider.GetRequiredService<IRepository<BusinessStatus>>();
			var businessRepo = scope.ServiceProvider.GetRequiredService<IRepository<Business>>();
			var packageRepo = scope.ServiceProvider.GetRequiredService<IRepository<Package>>();

			// --- Seed BusinessTypes ---
			var businessTypeNames = new[] { "Bakery", "Restaurant", "Grocery", "Cafe" };
			var allBusinessTypes = await businessTypeRepo.GetAllAsync();
			var businessTypes = new List<BusinessType>();
			foreach (var name in businessTypeNames)
			{
				var existing = allBusinessTypes.FirstOrDefault(t => t.Name == name);
				existing ??= await businessTypeRepo.AddAsync(new BusinessType { Uid = Guid.NewGuid(), Name = name });
				businessTypes.Add(existing!);
			}

			// --- Seed PackageTypes ---
			var packageTypeNames = new[] { "Bread", "Meal Box", "Produce", "Pastry" };
			var allPackageTypes = await packageTypeRepo.GetAllAsync();
			var packageTypes = new List<PackageType>();
			foreach (var name in packageTypeNames)
			{
				var existing = allPackageTypes.FirstOrDefault(t => t.Name == name);
				existing ??= await packageTypeRepo.AddAsync(new PackageType { Uid = Guid.NewGuid(), Name = name });
				packageTypes.Add(existing!);
			}

			// --- Seed BusinessStatuses ---
			var statusNames = new[] { "Pending", "Approved" };
			var allStatuses = await businessStatusRepo.GetAllAsync();
			var businessStatuses = new List<BusinessStatus>();
			foreach (var name in statusNames)
			{
				var existing = allStatuses.FirstOrDefault(s => s.Name == name);
				existing ??= await businessStatusRepo.AddAsync(new BusinessStatus { Uid = Guid.NewGuid(), Name = name });
				businessStatuses.Add(existing!);
			}
			var approvedStatus = businessStatuses.First(s => s.Name == "Approved");

			// --- Seed BusinessOwner users ---
			var ownerSeeds = new[]
			{
				new { Email = "owner1@ecomeal.test", Name = "Alice Baker" },
				new { Email = "owner2@ecomeal.test", Name = "Bob Grocer" },
				new { Email = "owner3@ecomeal.test", Name = "Carol Chef" }
		};

			var owners = new List<User>();
			foreach (var seed in ownerSeeds)
			{
				var user = await userManager.FindByEmailAsync(seed.Email);
				if (user is null)
				{
					user = new User { UserName = seed.Email, Email = seed.Email, EmailConfirmed = true };
					var result = await userManager.CreateAsync(user, "Password123!");
					if (result.Succeeded)
					{
						await userManager.AddToRoleAsync(user, "BusinessOwner");
					}
					else
					{
						Console.WriteLine($"[Seed] Failed to create {seed.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
						continue;
					}
				}
				owners.Add(user);
			}

			// --- Seed Businesses ---
			var businessSeeds = new[]
			{
				new { OwnerIndex = 0, Name = "Alice's Bakery", Address = "12 Flour St", TypeIndex = 0 },
				new { OwnerIndex = 1, Name = "Bob's Grocery", Address = "44 Market Ave", TypeIndex = 2 },
				new { OwnerIndex = 2, Name = "Carol's Kitchen", Address = "7 Spoon Rd", TypeIndex = 1 },
				new { OwnerIndex = 2, Name = "Carol's Cafe", Address = "9 Bean Blvd", TypeIndex = 3 }
		};

			var allBusinesses = await businessRepo.GetAllAsync();
			var businesses = new List<Business>();
			foreach (var seed in businessSeeds)
			{
				if (seed.OwnerIndex >= owners.Count) continue;

				var existing = allBusinesses.FirstOrDefault(b => b.Name == seed.Name);
				existing ??= await businessRepo.AddAsync(new Business
				{
					Uid = Guid.NewGuid(),
					Name = seed.Name,
					Address = seed.Address,
					Description = $"Welcome to {seed.Name}!",
					OwnerId = owners[seed.OwnerIndex].Id,
					BusinessTypeId = businessTypes[seed.TypeIndex].Uid,
					StatusId = approvedStatus.Uid
				});
				businesses.Add(existing!);
			}

			// --- Seed Packages ---
			var random = new Random();
			var allPackages = await packageRepo.GetAllAsync();
			foreach (var business in businesses)
			{
				if (allPackages.Any(p => p.BusinessId == business.Uid)) continue;

				for (int i = 1; i <= 3; i++)
				{
					await packageRepo.AddAsync(new Package
					{
						Uid = Guid.NewGuid(),
						BusinessId = business.Uid,
						PackageTypeId = packageTypes[random.Next(packageTypes.Count)].Uid,
						Name = $"{business.Name} Surprise Bag #{i}",
						Description = "A mix of leftover items rescued from waste.",
						Price = 4.99f + i,
						Quantity = (uint)random.Next(3, 15),
						PickupStart = DateTime.UtcNow.AddHours(2),
						PickupEnd = DateTime.UtcNow.AddHours(6),
						ImageUrl = null
					});
				}
			}

			Console.WriteLine("[Seed] Database seeding complete.");
		}
		app.Run();
	}
}
