using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcoMeal.Data;

public class EcoMealDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
	public EcoMealDbContext() { }
	public EcoMealDbContext(DbContextOptions<EcoMealDbContext> options) : base(options)
	{
	}

	public DbSet<Business> Businesses { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<BusinessType> BusinessTypes { get; set; }
	public DbSet<Package> Packages { get; set; }
	public DbSet<OrderPackage> OrderPackages { get; set; }
	public DbSet<PackageType> PackageTypes { get; set; }
	public DbSet<Status> Statuses { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfiguration(new BusinessTypeConfiguration());
		modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
		modelBuilder.ApplyConfiguration(new BusinessTypeTypeConfiguration());
		modelBuilder.ApplyConfiguration(new PackageTypeConfiguration());
		modelBuilder.ApplyConfiguration(new PackageTypeTypeConfiguration());
		modelBuilder.ApplyConfiguration(new OrderTypeConfiguration());
		modelBuilder.ApplyConfiguration(new StatusTypeConfiguration());
		modelBuilder.ApplyConfiguration(new OrderPackageTypeConfiguration());
	}
}
