using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
{
    public void Configure(EntityTypeBuilder<StockMovement> builder)
    {
        builder.ToTable("StockMovements").HasKey(sm => sm.Id);

        builder.Property(sm => sm.Id).HasColumnName("Id").IsRequired();
        builder.Property(sm => sm.ProductId).HasColumnName("ProductId").IsRequired();
        builder.Property(sm => sm.WarehouseId).HasColumnName("WarehouseId").IsRequired();
        builder.Property(sm => sm.WarehouseSlotId).HasColumnName("WarehouseSlotId").IsRequired();
        builder.Property(sm => sm.MovementType).HasColumnName("MovementType").IsRequired();
        builder.Property(sm => sm.Quantity).HasColumnName("Quantity").IsRequired();
        builder.Property(sm => sm.ReferenceNo).HasColumnName("ReferenceNo").IsRequired();
        builder.Property(sm => sm.Description).HasColumnName("Description").IsRequired();
        builder.Property(sm => sm.MovementDate).HasColumnName("MovementDate").IsRequired();
        builder.Property(sm => sm.CompanyId).HasColumnName("CompanyId").IsRequired();
        builder.Property(sm => sm.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(sm => sm.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(sm => sm.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(sm => sm.Product)
            .WithMany()
            .HasForeignKey(sm => sm.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(sm => sm.Warehouse)
            .WithMany()
            .HasForeignKey(sm => sm.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(sm => sm.WarehouseSlot)
            .WithMany()
            .HasForeignKey(sm => sm.WarehouseSlotId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(sm => sm.Company)
            .WithMany()
            .HasForeignKey(sm => sm.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(sm => !sm.DeletedDate.HasValue);
    }
}
