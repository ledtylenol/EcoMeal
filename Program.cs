using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using EcoMeal;
using EcoMeal.Data;
using EcoMeal.Components;

internal class Program
{
	private static async Task Main(string[] args)
	{
		// Replace with your connection string.
		var connectionString = "server=localhost;user=root;password=123;database=ef";
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddRazorComponents()
				.AddInteractiveServerComponents();

		builder.Services.AddBlazorBootstrap();
		builder.Services.AddBlazorBootstrap();

		var serverVersion = new MariaDbServerVersion(new Version(12, 3, 2));
		builder.Services.AddHttpClient();
		builder.Services.AddDbContext<EcoMealDbContext>(
				dbContextOptions =>
				{
					dbContextOptions.UseMySql(connectionString, serverVersion);
					dbContextOptions.LogTo(Console.WriteLine, LogLevel.Information)
					.EnableDetailedErrors();
				});
		builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
		builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new("http://localhost:5198")});
		var app = builder.Build();

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
		app.Run();
	}
}
