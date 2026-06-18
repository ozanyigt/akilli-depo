namespace Persistence.Seeding;

/// <summary>
/// Sabit GUID'ler — reviewer'ın frontend/Swagger'da aynı değerleri kullanabilmesi için.
/// </summary>
public static class DemoSeedIds
{
    public static readonly Guid TeknoCompanyId = Guid.Parse("0DAC2416-A292-4401-005C-08DECD47483A");
    public static readonly Guid GlobalCompanyId = Guid.Parse("D4A57AE8-A65B-47E2-005D-08DECD47483A");

    public static readonly Guid TeknoWarehouseId = Guid.Parse("97C62744-EA6C-4AEA-B01C-08DECD486689");
    public static readonly Guid GlobalWarehouseId = Guid.Parse("F6715D0C-2CB0-4AAE-B01B-08DECD486689");

    public static readonly Guid TeknoSlotId = Guid.Parse("1AD8110A-E8F4-4B2A-B01C-08DECD486689");
    public static readonly Guid GlobalSlotId = Guid.Parse("D2575C49-8A1E-4C9F-B01B-08DECD486689");

    public static readonly Guid TeknoProduct1Id = Guid.Parse("B1000001-0001-4001-8001-000000000001");
    public static readonly Guid TeknoProduct2Id = Guid.Parse("B1000001-0001-4001-8001-000000000002");
    public static readonly Guid TeknoStockInId = Guid.Parse("C1000001-0001-4001-8001-000000000001");
    public static readonly Guid TeknoStockOutId = Guid.Parse("C1000001-0001-4001-8001-000000000002");
    public static readonly Guid GlobalStockInId = Guid.Parse("C2000001-0001-4001-8001-000000000001");

    public static Guid TeknoProductId(int index) =>
        Guid.Parse($"B1000001-0001-4001-8001-{index:D12}");
}
