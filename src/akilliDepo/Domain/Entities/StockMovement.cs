using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class StockMovement : Entity<Guid>
    {
        public Guid ProductId { get; set; }
        public Guid WarehouseId { get; set; }
        public Guid WarehouseSlotId { get; set; }
        public string MovementType { get; set; }
        public int Quantity { get; set; }
        public string ReferenceNo { get; set; }
        public string Description { get; set; }
        public DateTime MovementDate { get; set; }
        public Guid CompanyId { get; set; }

        public Product Product { get; set; }
        public Warehouse Warehouse { get; set; }
        public WarehouseSlot WarehouseSlot { get; set; }
        public Company Company { get; set; }
    }
}
