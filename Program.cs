using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using EcoMeal;
using EcoMeal.Data;
using EcoMeal.Components;
using Microsoft.AspNetCore.Identity;

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

		app.UseAntiforgery();
		app.MapStaticAssets();
		app.UseStaticFiles();
		app.MapRazorComponents<App>()
				.AddInteractiveServerRenderMode();

		app.MapControllers();
		app.UseAuthentication();
		app.UseAuthorization();
		app.Run();
	}
}
