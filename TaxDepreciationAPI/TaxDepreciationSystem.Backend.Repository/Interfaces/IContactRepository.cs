using TaxDepreciationSystem.Backend.Repository.Generic_Repository;
using TaxDepreciationSystem.Backend.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaxDepreciationSystem.Backend.Repository.Interfaces
{
    public interface IContactRepository : IDbRepository<Contact>
    {
        Task<List<Contact>> GetContacts();

    }
}
