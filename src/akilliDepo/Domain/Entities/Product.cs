using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Product : Entity<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        public Guid CompanyId { get; set; }
        public decimal MinStockLevel { get; set; }
        public bool IsActive { get; set; }

        public Company Company { get; set; }
    }
}
