using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.ToTable("Warehouses").HasKey(w => w.Id);

        builder.Property(w => w.Id).HasColumnName("Id").IsRequired();
        builder.Property(w => w.Name).HasColumnName("Name").IsRequired();
        builder.Property(w => w.Code).HasColumnName("Code").IsRequired();
        builder.Property(w => w.Location).HasColumnName("Location").IsRequired();
        builder.Property(w => w.Capacity).HasColumnName("Capacity").IsRequired();
        builder.Property(w => w.IsActive).HasColumnName("IsActive").IsRequired();
        builder.Property(w => w.CompanyId).HasColumnName("CompanyId").IsRequired();
        builder.Property(w => w.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(w => w.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(w => w.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(w => w.Company)
            .WithMany()
            .HasForeignKey(w => w.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(w => !w.DeletedDate.HasValue);
    }
}
