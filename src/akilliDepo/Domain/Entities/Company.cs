using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Company : Entity<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public Guid CompanyId { get; set; }
    }
}
