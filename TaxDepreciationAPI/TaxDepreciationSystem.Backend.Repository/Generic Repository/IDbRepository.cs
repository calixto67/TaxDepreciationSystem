using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxDepreciationSystem.Backend.Repository.Generic_Repository
{
    public interface IDbRepository<T>
    {
        IQueryable<T> Get();

        T Get(int id);

        Task<List<T>> GetAsync();

        Task<T> GetAsync(int id);

        Task<List<T>> SelectAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate);

        void Create(T record);

        void Update(T record);

        void Delete(int id);

        void Remove(int id);

        int Save();

        Task<int> SaveAsync();
    }
}
