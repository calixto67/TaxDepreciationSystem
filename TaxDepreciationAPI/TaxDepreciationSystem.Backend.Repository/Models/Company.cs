using TaxDepreciationSystem.Backend.Repository.Generic_Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaxDepreciationSystem.Backend.Repository.Models
{
    public partial class Company : IDataAccessEntity
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
