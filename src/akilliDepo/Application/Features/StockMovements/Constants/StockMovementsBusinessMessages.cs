namespace Application.Features.StockMovements.Constants;

public static class StockMovementsBusinessMessages
{
    public const string SectionName = "StockMovement";

    public const string StockMovementNotExists = "StockMovementNotExists";
    public const string InvalidMovementType = "InvalidMovementType";
    public const string QuantityMustBePositive = "QuantityMustBePositive";
    public const string InsufficientStock = "InsufficientStock";
    public const string CapacityExceeded = "CapacityExceeded";
    public const string WarehouseSlotMismatch = "WarehouseSlotMismatch";
}