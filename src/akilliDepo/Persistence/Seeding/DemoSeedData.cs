using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Seeding;

public static class DemoSeedData
{
    public static readonly DateTime CreatedAt = new(2026, 6, 18, 12, 0, 0, DateTimeKind.Utc);

    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>().HasData(
            new Company
            {
                Id = DemoSeedIds.TeknoCompanyId,
                CompanyId = DemoSeedIds.TeknoCompanyId,
                Name = "Tekno Çözüm A.Ş.",
                Code = "TEKNO",
                Email = "info@tekno-cozum.local",
                Phone = "+90 212 555 0101",
                IsActive = true,
                CreatedDate = CreatedAt,
            },
            new Company
            {
                Id = DemoSeedIds.GlobalCompanyId,
                CompanyId = DemoSeedIds.GlobalCompanyId,
                Name = "Global Lojistik Ltd.",
                Code = "GLOBAL",
                Email = "info@global-lojistik.local",
                Phone = "+90 216 555 0202",
                IsActive = true,
                CreatedDate = CreatedAt,
            }
        );

        modelBuilder.Entity<Warehouse>().HasData(
            new Warehouse
            {
                Id = DemoSeedIds.TeknoWarehouseId,
                CompanyId = DemoSeedIds.TeknoCompanyId,
                Name = "Tekno Ana Depo",
                Code = "TEKNO-ANA",
                Location = "İstanbul / Tuzla",
                Capacity = 10000,
                IsActive = true,
                CreatedDate = CreatedAt,
            },
            new Warehouse
            {
                Id = DemoSeedIds.GlobalWarehouseId,
                CompanyId = DemoSeedIds.GlobalCompanyId,
                Name = "Global Merkez Depo",
                Code = "GLB-MRK",
                Location = "Kocaeli / Gebze",
                Capacity = 8000,
                IsActive = true,
                CreatedDate = CreatedAt,
            }
        );

        modelBuilder.Entity<WarehouseSlot>().HasData(
            new WarehouseSlot
            {
                Id = DemoSeedIds.TeknoSlotId,
                CompanyId = DemoSeedIds.TeknoCompanyId,
                WarehouseId = DemoSeedIds.TeknoWarehouseId,
                Code = "A-01-01",
                Zone = "A",
                Capacity = 500,
                CurrentStock = 40,
                IsActive = true,
                CreatedDate = CreatedAt,
            },
            new WarehouseSlot
            {
                Id = DemoSeedIds.GlobalSlotId,
                CompanyId = DemoSeedIds.GlobalCompanyId,
                WarehouseId = DemoSeedIds.GlobalWarehouseId,
                Code = "B-01-01",
                Zone = "B",
                Capacity = 300,
                CurrentStock = 25,
                IsActive = true,
                CreatedDate = CreatedAt,
            }
        );

        modelBuilder.Entity<Product>().HasData(BuildTeknoProducts().Concat(BuildGlobalProducts()).ToArray());

        modelBuilder.Entity<StockMovement>().HasData(
            new StockMovement
            {
                Id = DemoSeedIds.TeknoStockInId,
                CompanyId = DemoSeedIds.TeknoCompanyId,
                ProductId = DemoSeedIds.TeknoProduct1Id,
                WarehouseId = DemoSeedIds.TeknoWarehouseId,
                WarehouseSlotId = DemoSeedIds.TeknoSlotId,
                MovementType = "IN",
                Quantity = 50,
                ReferenceNo = "GRN-TEKNO-001",
                Description = "Seed: ilk depo girişi",
                MovementDate = CreatedAt,
                CreatedDate = CreatedAt,
            },
            new StockMovement
            {
                Id = DemoSeedIds.TeknoStockOutId,
                CompanyId = DemoSeedIds.TeknoCompanyId,
                ProductId = DemoSeedIds.TeknoProduct1Id,
                WarehouseId = DemoSeedIds.TeknoWarehouseId,
                WarehouseSlotId = DemoSeedIds.TeknoSlotId,
                MovementType = "OUT",
                Quantity = 10,
                ReferenceNo = "ISS-TEKNO-001",
                Description = "Seed: örnek depo çıkışı",
                MovementDate = CreatedAt.AddHours(2),
                CreatedDate = CreatedAt.AddHours(2),
            },
            new StockMovement
            {
                Id = DemoSeedIds.GlobalStockInId,
                CompanyId = DemoSeedIds.GlobalCompanyId,
                ProductId = Guid.Parse("B2000001-0001-4001-8001-000000000001"),
                WarehouseId = DemoSeedIds.GlobalWarehouseId,
                WarehouseSlotId = DemoSeedIds.GlobalSlotId,
                MovementType = "IN",
                Quantity = 25,
                ReferenceNo = "GRN-GLB-001",
                Description = "Seed: Global lojistik giriş",
                MovementDate = CreatedAt,
                CreatedDate = CreatedAt,
            }
        );
    }

    private static IEnumerable<Product> BuildTeknoProducts()
    {
        (string code, string name, string unit, decimal minStock)[] items =
        [
            ("TKN-PRD-001", "Endüstriyel Ethernet Switch 8 Port", "Adet", 5),
            ("TKN-PRD-002", "RFID Etiket 50x30 mm", "Paket", 20),
            ("TKN-PRD-003", "Barkod Okuyucu El Tipi", "Adet", 3),
            ("TKN-PRD-004", "Termal Yazıcı Rulo", "Kutu", 10),
            ("TKN-PRD-005", "Lityum Forklift Bataryası 48V", "Adet", 2),
            ("TKN-PRD-006", "Paletleme Streç Film", "Rulo", 30),
            ("TKN-PRD-007", "Koruyucu İş Eldiveni", "Çift", 50),
            ("TKN-PRD-008", "PP Kayış 16mm", "Metre", 100),
            ("TKN-PRD-009", "Depo Raf Etiketi A4", "Paket", 15),
            ("TKN-PRD-010", "IoT Sıcaklık Sensörü", "Adet", 8),
            ("TKN-PRD-011", "Akıllı Depo LED Aydınlatma", "Adet", 12),
            ("TKN-PRD-012", "Yangın Söndürme Tüpü 6kg", "Adet", 6),
        ];

        for (int i = 0; i < items.Length; i++)
        {
            var item = items[i];
            yield return new Product
            {
                Id = DemoSeedIds.TeknoProductId(i + 1),
                CompanyId = DemoSeedIds.TeknoCompanyId,
                Code = item.code,
                Name = item.name,
                Unit = item.unit,
                Description = $"{item.name} — demo seed ürünü",
                MinStockLevel = item.minStock,
                IsActive = true,
                CreatedDate = CreatedAt,
            };
        }
    }

    private static IEnumerable<Product> BuildGlobalProducts()
    {
        (Guid id, string code, string name)[] items =
        [
            (Guid.Parse("B2000001-0001-4001-8001-000000000001"), "GLB-PRD-001", "Kargo Paleti Euro Tip"),
            (Guid.Parse("B2000001-0001-4001-8001-000000000002"), "GLB-PRD-002", "Soğuk Zincir Kutusu"),
            (Guid.Parse("B2000001-0001-4001-8001-000000000003"), "GLB-PRD-003", "GPS Takip Cihazı"),
        ];

        foreach (var item in items)
        {
            yield return new Product
            {
                Id = item.id,
                CompanyId = DemoSeedIds.GlobalCompanyId,
                Code = item.code,
                Name = item.name,
                Unit = "Adet",
                Description = $"{item.name} — demo seed ürünü",
                MinStockLevel = 5,
                IsActive = true,
                CreatedDate = CreatedAt,
            };
        }
    }
}
