using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class WarehouseSlot : Entity<Guid>
    {
        public Guid WarehouseId { get; set; }
        public string Code { get; set; }
        public string Zone { get; set; }
        public int Capacity { get; set; }
        public int CurrentStock { get; set; }
        public bool IsActive { get; set; }
        public Guid CompanyId { get; set; }

        public Warehouse Warehouse { get; set; }
        public Company Company { get; set; }
    }
}
