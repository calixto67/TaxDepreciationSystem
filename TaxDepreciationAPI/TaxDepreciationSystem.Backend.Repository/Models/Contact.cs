using TaxDepreciationSystem.Backend.Repository.Generic_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxDepreciationSystem.Backend.Repository.Models
{
    public partial class Contact : IDataAccessEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

    }
}
