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

		var serverVersion = new MariaDbServerVersion(new Version(12, 3, 2));
		services.AddHttpClient();
		services.AddDbContext<EcoMealDbContext>(
				dbContextOptions =>
				{
					dbContextOptions.UseMySql(connectionString, serverVersion);
					dbContextOptions.LogTo(Console.WriteLine, LogLevel.Information)
					.EnableDetailedErrors();
				});
		services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
		services.AddScoped(sp => new HttpClient { BaseAddress = new("http://localhost:5198") });
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
				return Results.Redirect("/login?error=1");

			var result = await signInManager.PasswordSignInAsync(user, password, isPersistent: true, lockoutOnFailure: false);
			if (!result.Succeeded)
				return Results.Redirect("/login?error=1");

			return Results.Redirect(string.IsNullOrWhiteSpace(returnUrl) ? "/" : returnUrl);
		}).DisableAntiforgery();

		app.MapPost("/logout", async (HttpContext httpContext, SignInManager<User> signInManager) =>
		{
			await signInManager.SignOutAsync();
			return Results.Redirect("/login");
		}).DisableAntiforgery();
		app.Run();
	}
}
