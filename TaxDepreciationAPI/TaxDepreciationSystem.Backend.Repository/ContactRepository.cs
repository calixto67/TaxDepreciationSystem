using TaxDepreciationSystem.Backend.Repository.Generic_Repository;
using TaxDepreciationSystem.Backend.Repository.Interfaces;
using TaxDepreciationSystem.Backend.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxDepreciationSystem.Backend.Repository
{
    public class ContactRepository : DBRepository<Contact>, IContactRepository
    {
        public ContactRepository(BMTContext bmtContext) : base(bmtContext) 
        {

        }

        public async Task<List<Contact>> GetContacts() {
            return await this.context.Contact.Where(m => m.IsDeleted == false)
                                             .ToListAsync();
        }
    }
}
