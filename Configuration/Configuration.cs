using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BusinessTypeConfiguration : IEntityTypeConfiguration<Business>
{
	public void Configure(EntityTypeBuilder<Business> builder)
	{
		builder.HasKey(p => p.Uid);

		builder.HasOne(p => p.BusinessType).WithOne()
			.HasForeignKey<Business>(p => p.BusinessTypeId);

		builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
		builder.Property(p => p.Description).HasMaxLength(500);
		builder.Property(p => p.Address).HasMaxLength(100);
		builder.Property(p => p.ImageUrl).HasMaxLength(100);
	}
}
public class BusinessTypeTypeConfiguration : IEntityTypeConfiguration<BusinessType>
{
	public void Configure(EntityTypeBuilder<BusinessType> builder)
	{
		builder.HasKey(p => p.Uid);

		builder.Property(p => p.Name).IsRequired().HasMaxLength(50);

		builder.HasMany(p => p.Businesses).WithOne(p => p.BusinessType).HasForeignKey(p => p.BusinessTypeId);
	}
}
public class OrderTypeConfiguration : IEntityTypeConfiguration<Order>
{
	public void Configure(EntityTypeBuilder<Order> builder)
	{
		builder.HasKey(p => p.Uid);

		builder.HasOne(p => p.User).WithMany(u => u.Orders).HasForeignKey(p => p.UserId).IsRequired();
		builder.HasOne(p => p.Business).WithMany(b => b.Orders).HasForeignKey(p => p.BusinessId).IsRequired();
		builder.HasOne(p => p.Status).WithMany(s => s.Orders).HasForeignKey(p => p.StatusId).IsRequired();

		builder.HasMany(p => p.OrderPackages).WithOne(op => op.Order).HasForeignKey(p => p.OrderId);
	}
}
public class OrderPackageTypeConfiguration : IEntityTypeConfiguration<OrderPackage>
{
	public void Configure(EntityTypeBuilder<OrderPackage> builder)
	{
		builder.HasKey(p => new { p.OrderId, p.PackageId });

		builder.HasOne(p => p.Order).WithMany(o => o.OrderPackages).HasForeignKey(p => p.OrderId).IsRequired();
		builder.HasOne(p => p.Package).WithMany(o => o.OrderPackages).HasForeignKey(p => p.PackageId).IsRequired();

		builder.Property(p => p.Quantity).IsRequired();
	}
}
public class PackageTypeConfiguration : IEntityTypeConfiguration<Package>
{
	public void Configure(EntityTypeBuilder<Package> builder)
	{
		builder.HasKey(p => p.Uid);
		builder.HasOne(p => p.Business).WithMany(p => p.Packages)
			.HasForeignKey(p => p.BusinessId).IsRequired();

		builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
		builder.Property(p => p.Description).HasMaxLength(100);
		builder.Property(p => p.ImageUrl).HasMaxLength(100);
		builder.Property(p => p.PickupEnd).IsRequired();
		builder.Property(p => p.PickupStart).IsRequired();
		builder.Property(p => p.Quantity).IsRequired();
		builder.Property(p => p.Price).IsRequired();

		builder.HasOne(p => p.PackageType).WithMany(t => t.Packages).HasForeignKey(p => p.PackageTypeId).IsRequired();

	}
}
public class PackageTypeTypeConfiguration : IEntityTypeConfiguration<PackageType>
{
	public void Configure(EntityTypeBuilder<PackageType> builder)
	{
		builder.HasKey(p => p.Uid);

		builder.Property(p => p.Name).IsRequired().HasMaxLength(50);

		builder.HasMany(p => p.Packages).WithOne(p => p.PackageType).HasForeignKey(p => p.PackageTypeId);
	}
}
public class RoleTypeConfiguration : IEntityTypeConfiguration<Role>
{
	public void Configure(EntityTypeBuilder<Role> builder)
	{
		builder.HasKey(p => p.Uid);

		builder.Property(p => p.Name).IsRequired().HasMaxLength(50);

		builder.HasMany(p => p.Users).WithOne(p => p.Role).HasForeignKey(p => p.RoleId);
	}
}
public class StatusTypeConfiguration : IEntityTypeConfiguration<Status>
{
	public void Configure(EntityTypeBuilder<Status> builder)
	{
		builder.HasKey(p => p.Uid);

		builder.Property(p => p.Name).IsRequired().HasMaxLength(50);

		builder.HasMany(p => p.Orders).WithOne(p => p.Status).HasForeignKey(p => p.StatusId);
	}
}
public class UserTypeConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasKey(p => p.Uid);
		builder.HasOne(p => p.Role).WithMany(r => r.Users).HasForeignKey(p => p.RoleId).IsRequired();

		builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
		builder.Property(p => p.Email).HasMaxLength(100);
		builder.Property(p => p.Password).HasMaxLength(100);

		builder.HasMany(p => p.Orders).WithOne(o => o.User).HasForeignKey(o => o.UserId);
	}
}
