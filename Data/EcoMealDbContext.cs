using Microsoft.EntityFrameworkCore;

namespace EcoMeal.Data;

public class EcoMealDbContext : DbContext
{
	public EcoMealDbContext() { }
	public EcoMealDbContext(DbContextOptions<EcoMealDbContext> options) : base(options)
	{
	}

	public DbSet<User> Users { get; set; }
	public DbSet<Business> Businesses { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<Role> Roles { get; set; }
	public DbSet<BusinessType> BusinessTypes { get; set; }
	public DbSet<Package> Packages { get; set; }
	public DbSet<OrderPackage> OrderPackages { get; set; }
	public DbSet<PackageType> PackageTypes { get; set; }
	public DbSet<Status> Statuses { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new RoleTypeConfiguration());
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
