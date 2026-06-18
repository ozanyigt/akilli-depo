using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class WarehouseSlotConfiguration : IEntityTypeConfiguration<WarehouseSlot>
{
    public void Configure(EntityTypeBuilder<WarehouseSlot> builder)
    {
        builder.ToTable("WarehouseSlots").HasKey(ws => ws.Id);

        builder.Property(ws => ws.Id).HasColumnName("Id").IsRequired();
        builder.Property(ws => ws.WarehouseId).HasColumnName("WarehouseId").IsRequired();
        builder.Property(ws => ws.Code).HasColumnName("Code").IsRequired();
        builder.Property(ws => ws.Zone).HasColumnName("Zone").IsRequired();
        builder.Property(ws => ws.Capacity).HasColumnName("Capacity").IsRequired();
        builder.Property(ws => ws.CurrentStock).HasColumnName("CurrentStock").IsRequired();
        builder.Property(ws => ws.IsActive).HasColumnName("IsActive").IsRequired();
        builder.Property(ws => ws.CompanyId).HasColumnName("CompanyId").IsRequired();
        builder.Property(ws => ws.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ws => ws.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ws => ws.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(ws => ws.Warehouse)
            .WithMany()
            .HasForeignKey(ws => ws.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ws => ws.Company)
            .WithMany()
            .HasForeignKey(ws => ws.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(ws => !ws.DeletedDate.HasValue);
    }
}
