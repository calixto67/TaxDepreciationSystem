using System;
using System.Collections.Generic;
using System.Text;

namespace TaxDepreciationSystem.Backend.Repository.Generic_Repository
{
    public interface IDataAccessEntity
    {
        int Id { get; set; }

        int CreatedBy { get; set; }

        int UpdatedBy { get; set; }

        DateTime CreatedDate { get; set; }

        DateTime UpdatedDate { get; set; }

        bool IsDeleted { get; set; }
    }
}
