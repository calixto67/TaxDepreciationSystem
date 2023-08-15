using TaxDepreciationSystem.Backend.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TaxDepreciationSystem.Backend.Repository.Generic_Repository
{
    public class DBRepository<T> : IDbRepository<T> where T : class, IDataAccessEntity
    {
        protected BMTContext context;

        public DBRepository(BMTContext bmtContext)
        {
            context = bmtContext;
        }
        public void Create(T record)
        {
            record.CreatedDate = DateTime.Now;
            record.UpdatedDate = record.CreatedDate;
            record.IsDeleted = false;
            context.Add(record);
        }

        public void Delete(int id)
        {
            var record = Get(id);

            if (record != null)
            {
                record.UpdatedDate = DateTime.Now;
                record.IsDeleted = true;
            }
        }

        public void Remove(int id)
        {
            var record = Get(id);
            context.Remove(record);
        }

        public IQueryable<T> Get()
        {
            return context.Set<T>().Where(e => e.IsDeleted == false);
        }

        public async Task<List<T>> GetAsync()
        {
            return await context.Set<T>().Where(e => e.IsDeleted == false).ToListAsync();
        }

        public async Task<List<T>> SelectAsync(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().Where(predicate).ToListAsync();
        }

        public T Get(int id)
        {
            return Get().SingleOrDefault(e => e.Id == id);
        }

        public async Task<T> GetAsync(int id)
        {
            return await Get().SingleOrDefaultAsync(e => e.Id == id);
        }
        public int Save()
        {
            return context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return context.SaveChangesAsync();
        }

        public void Update(T record)
        {
            record.UpdatedDate = DateTime.Now;
            context.Set<T>().Attach(record);
            context.Entry(record).State = EntityState.Modified;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }
    }

}
