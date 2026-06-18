using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Warehouse : Entity<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public bool IsActive { get; set; }
        public Guid CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
