using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedDemoData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Code", "CompanyId", "CreatedDate", "DeletedDate", "Email", "IsActive", "Name", "Phone", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("0dac2416-a292-4401-005c-08decd47483a"), "TEKNO", new Guid("0dac2416-a292-4401-005c-08decd47483a"), new DateTime(2026, 6, 18, 12, 0, 0, 0, DateTimeKind.Utc), null, "info@tekno-cozum.local", true, "Tekno Çözüm A.Ş.", "+90 212 555 0101", null },
                    { new Guid("d4a57ae8-a65b-47e2-005d-08decd47483a"), "GLOBAL", new Guid("d4a57ae8-a65b-47e2-005d-08decd47483a"), new DateTime(2026, 6, 18, 12, 0, 0, 0, DateTimeKind.Utc), null, "info@global-lojistik.local", true, "Global Lojistik Ltd.", "+90 216 555 0202", null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Code", "CompanyId", "CreatedDate", "DeletedDate", "Description", "IsActive", "MinStockLevel", "Name", "Unit", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("b1000001-0001-4001-8001-000000000001"), "TKN-PRD-001", new Guid("0dac2416-a292-4401-005c-08decd47483a"), new DateTime(2026, 6, 18, 12, 0, 0, 0, DateTimeKind.Utc), null, "Endüstriyel Ethernet Switch 8 Port — demo seed ürünü", true, 5m, "Endüstriyel Ethernet Switch 8 Port", "Adet", null },
                    { new Guid("b1000001-0001-4001-8001-000000000002"), "TKN-PRD-002", new Guid("0dac2416-a292-4401-005c-08decd47483a"), new DateTime(2026, 6, 18, 12, 0, 0, 0, DateTimeKind.Utc), null, "RFID Etiket 50x30 mm — demo seed ürünü", true, 20m, "RFID Etiket 50x30 mm", "Paket", null },
                    { new Guid("b1000001-0001-4001-8001-000000000003"), "TKN-PRD-003", new Guid("0dac2416-a292-4401-005c-08decd47483a"), new DateTime(2026, 6, 18, 12, 0, 0, 0, DateTimeKind.Utc), null, "Barkod Okuyucu El Tipi — demo seed ürünü", true, 3m, "Barkod Okuyucu El Tipi", "Adet", null },
                    { new Guid("b1000001-0001-4001-8001-000000000004"), "TKN-PRD-004", new Guid("0dac2416-a292-4401-005c-08decd47483a"), new DateTime(2026, 6, 18, 12, 0, 0, 0, DateTimeKind.Utc), null, "Termal Yazıcı Rulo — demo seed ürünü", true, 10m, "Termal Yazıcı Rulo", "Kutu", null },
                    { new Guid("b1000001-0001-4001-8001-000000000005"), "TKN-PRD-005", new Guid("0dac2416-a292-4401-005c-08decd47483a"), new DateTime(2026, 6, 18, 12, 0, 0, 0, DateTimeKind.Utc), null, "Lityum Forklift Bataryası 48V — demo seed ürünü", true, 2m, "Lityum Forklift Bataryası 48V", "Adet", null },
                    { new Guid("b1000001-0001-4001-8001-000000000006"), "TKN-PRD-006", new Guid("0dac2416-a292-4401-005c-08decd47483a"), new DateTime(2026, 6, 18, 12, 0, 0, 0, DateTimeKind.Utc), null, "Paletleme Streç Film — demo seed ürünü", true, 30m, "Paletleme Streç Film", "Rulo", null },
                    { new Guid("b1000001-0001-4001-8001-000000000007"), "TKN-PRD-007", new Guid("0dac2416-a292-4401-005c-08decd47483a"), new DateTime(2026, 6, 18, 12, 0, 0, 0, DateTimeKind.Utc), null, "Koruyucu İş Eldiveni — demo seed ürünü", true, 50m, "Koruyucu İş Eldiveni", "Çift", null },
                    { new Guid("b1000001-0001-4001-8001-000000000008"), "TKN-PRD-008", new Guid("0dac2416-a292-4401-005c-08decd47483a"), new DateTime(2026, 6, 18, 12, 0, 0, 0, DateTimeKind.Utc), null, "PP Kayış 16mm — demo seed ürünü", true, 100m, "PP Kayış 16mm", "Metre", null },
                    { new Guid("b1000001-0001-4001-8001-000000000009"), "TKN-PRD-009", new Guid("0dac2416-a292-4401-005c-08decd47483a"), new DateTime(2026, 6, 18, 12, 0, 0, 0, DateTimeKind.Utc), null, "Depo Raf Etiketi A4 — demo seed ürünü", true, 15m, "Depo Raf Etiketi A4", "Paket", null },
                    { new Guid("b1000001-0001-4001-8001-000000000010"), "TKN-PRD-010", new Guid("0dac2416-a292-4401-005c-08decd47483a"), new DateTime(2026, 6, 18, 12, 0, 0, 0, DateTimeKind.Utc), null, "IoT Sıcaklık Sensörü — demo seed ürünü", true, 8m, "IoT Sıcaklık Sensörü", "Adet", null },
                    { new Guid("b1000001-0001-4001-8001-000000000011"), "TKN-PRD-011", new Guid("0dac2416-a292-4401-005c-08decd47483a"), new DateTime(2026, 6, 18, 12, 0, 0, 0, DateTimeKind.Utc), null, "Akıllı Depo LED Aydınlatma — demo seed ürünü", true, 12m, "Akıllı Depo LED Aydınlatma", "Adet", null },
                    { new Guid("b1000001-0001-4001-8001-000000000012"), "TKN-PRD-012", new Guid("0dac2416-a292-4401-005c-08decd47483a"), new DateTime(2026, 6, 18, 12, 0, 0, 0, DateTimeKind.Utc), null, "Yangın Söndürme Tüpü 6kg — demo seed ürünü", true, 6m, "Yangın Söndürme Tüpü 6kg", "Adet", null },
                    { new Guid("b2000001-0001-4001-8001-000000000001"), "GLB-PRD-001", new Guid("d4a57ae8-a65b-47e2-005d-08decd47483a"), new DateTime(2026, 6, 18, 12, 0, 0, 0, DateTimeKind.Utc), null, "Kargo Paleti Euro Tip — demo seed ürünü", true, 5m, "Kargo Paleti Euro Tip", "Adet", null },
                    { new Guid("b2000001-0001-4001-8001-000000000002"), "GLB-PRD-002", new Guid("d4a57ae8-a65b-47e2-005d-08decd47483a"), new DateTime(2026, 6, 18, 12, 0, 0, 0, DateTimeKind.Utc), null, "Soğuk Zincir Kutusu — demo seed ürünü", true, 5m, "Soğuk Zincir Kutusu", "Adet", null },
                    { new Guid("b2000001-0001-4001-8001-000000000003"), "GLB-PRD-003", new Guid("d4a57ae8-a65b-47e2-005d-08decd47483a"), new DateTime(2026, 6, 18, 12, 0, 0, 0, DateTimeKind.Utc), null, "GPS Takip Cihazı — demo seed ürünü", true, 5m, "GPS Takip Cihazı", "Adet", null }
                });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "Capacity", "Code", "CompanyId", "CreatedDate", "DeletedDate", "IsActive", "Location", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("97c62744-ea6c-4aea-b01c-08decd486689"), 10000, "TEKNO-ANA", new Guid("0dac2416-a292-4401-005c-08decd47483a"), new DateTime(2026, 6, 18, 12, 0, 0, 0, DateTimeKind.Utc), null, true, "İstanbul / Tuzla", "Tekno Ana Depo", null },
                    { new Guid("f6715d0c-2cb0-4aae-b01b-08decd486689"), 8000, "GLB-MRK", new Guid("d4a57ae8-a65b-47e2-005d-08decd47483a"), new DateTime(2026, 6, 18, 12, 0, 0, 0, DateTimeKind.Utc), null, true, "Kocaeli / Gebze", "Global Merkez Depo", null }
                });

            migrationBuilder.InsertData(
                table: "WarehouseSlots",
                columns: new[] { "Id", "Capacity", "Code", "CompanyId", "CreatedDate", "CurrentStock", "DeletedDate", "IsActive", "UpdatedDate", "WarehouseId", "Zone" },
                values: new object[,]
                {
                    { new Guid("1ad8110a-e8f4-4b2a-b01c-08decd486689"), 500, "A-01-01", new Guid("0dac2416-a292-4401-005c-08decd47483a"), new DateTime(2026, 6, 18, 12, 0, 0, 0, DateTimeKind.Utc), 40, null, true, null, new Guid("97c62744-ea6c-4aea-b01c-08decd486689"), "A" },
                    { new Guid("d2575c49-8a1e-4c9f-b01b-08decd486689"), 300, "B-01-01", new Guid("d4a57ae8-a65b-47e2-005d-08decd47483a"), new DateTime(2026, 6, 18, 12, 0, 0, 0, DateTimeKind.Utc), 25, null, true, null, new Guid("f6715d0c-2cb0-4aae-b01b-08decd486689"), "B" }
                });

            migrationBuilder.InsertData(
                table: "StockMovements",
                columns: new[] { "Id", "CompanyId", "CreatedDate", "DeletedDate", "Description", "MovementDate", "MovementType", "ProductId", "Quantity", "ReferenceNo", "UpdatedDate", "WarehouseId", "WarehouseSlotId" },
                values: new object[,]
                {
                    { new Guid("c1000001-0001-4001-8001-000000000001"), new Guid("0dac2416-a292-4401-005c-08decd47483a"), new DateTime(2026, 6, 18, 12, 0, 0, 0, DateTimeKind.Utc), null, "Seed: ilk depo girişi", new DateTime(2026, 6, 18, 12, 0, 0, 0, DateTimeKind.Utc), "IN", new Guid("b1000001-0001-4001-8001-000000000001"), 50, "GRN-TEKNO-001", null, new Guid("97c62744-ea6c-4aea-b01c-08decd486689"), new Guid("1ad8110a-e8f4-4b2a-b01c-08decd486689") },
                    { new Guid("c1000001-0001-4001-8001-000000000002"), new Guid("0dac2416-a292-4401-005c-08decd47483a"), new DateTime(2026, 6, 18, 14, 0, 0, 0, DateTimeKind.Utc), null, "Seed: örnek depo çıkışı", new DateTime(2026, 6, 18, 14, 0, 0, 0, DateTimeKind.Utc), "OUT", new Guid("b1000001-0001-4001-8001-000000000001"), 10, "ISS-TEKNO-001", null, new Guid("97c62744-ea6c-4aea-b01c-08decd486689"), new Guid("1ad8110a-e8f4-4b2a-b01c-08decd486689") },
                    { new Guid("c2000001-0001-4001-8001-000000000001"), new Guid("d4a57ae8-a65b-47e2-005d-08decd47483a"), new DateTime(2026, 6, 18, 12, 0, 0, 0, DateTimeKind.Utc), null, "Seed: Global lojistik giriş", new DateTime(2026, 6, 18, 12, 0, 0, 0, DateTimeKind.Utc), "IN", new Guid("b2000001-0001-4001-8001-000000000001"), 25, "GRN-GLB-001", null, new Guid("f6715d0c-2cb0-4aae-b01b-08decd486689"), new Guid("d2575c49-8a1e-4c9f-b01b-08decd486689") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b1000001-0001-4001-8001-000000000002"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b1000001-0001-4001-8001-000000000003"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b1000001-0001-4001-8001-000000000004"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b1000001-0001-4001-8001-000000000005"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b1000001-0001-4001-8001-000000000006"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b1000001-0001-4001-8001-000000000007"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b1000001-0001-4001-8001-000000000008"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b1000001-0001-4001-8001-000000000009"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b1000001-0001-4001-8001-000000000010"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b1000001-0001-4001-8001-000000000011"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b1000001-0001-4001-8001-000000000012"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b2000001-0001-4001-8001-000000000002"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b2000001-0001-4001-8001-000000000003"));

            migrationBuilder.DeleteData(
                table: "StockMovements",
                keyColumn: "Id",
                keyValue: new Guid("c1000001-0001-4001-8001-000000000001"));

            migrationBuilder.DeleteData(
                table: "StockMovements",
                keyColumn: "Id",
                keyValue: new Guid("c1000001-0001-4001-8001-000000000002"));

            migrationBuilder.DeleteData(
                table: "StockMovements",
                keyColumn: "Id",
                keyValue: new Guid("c2000001-0001-4001-8001-000000000001"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b1000001-0001-4001-8001-000000000001"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b2000001-0001-4001-8001-000000000001"));

            migrationBuilder.DeleteData(
                table: "WarehouseSlots",
                keyColumn: "Id",
                keyValue: new Guid("1ad8110a-e8f4-4b2a-b01c-08decd486689"));

            migrationBuilder.DeleteData(
                table: "WarehouseSlots",
                keyColumn: "Id",
                keyValue: new Guid("d2575c49-8a1e-4c9f-b01b-08decd486689"));

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("97c62744-ea6c-4aea-b01c-08decd486689"));

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("f6715d0c-2cb0-4aae-b01b-08decd486689"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("0dac2416-a292-4401-005c-08decd47483a"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("d4a57ae8-a65b-47e2-005d-08decd47483a"));
        }
    }
}
